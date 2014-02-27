﻿/*
 * Copyright 2013 Splunk, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"): you may
 * not use this file except in compliance with the License. You may obtain
 * a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

// Summary of class relationships and control flow
// 
// All result readers support both the Iterator interface and
// getNextEvent method. They share the same underlying implementation
// of getNextElement(). The iterator interface is supported through
// the base class, StreamIterableBase (which is also used by
// multi result readers).
// 
// Some result readers support multiple result sets in the input stream.
// A result set can be skipped, or combined with the
// previous result set with newer events returned through the same
// iterator used for the older events even through they are in different result
// sets.
// 
// Such a result reader is also used by a multi result reader which
// returns an iterator over the result sets, with one result set returned
// in one iteration, as SearchResults. SearchResults is an interface consisting
// of getters of preview flag, field name list, and an iterator over events.
// Unlike ResultReader, SearchResults does not have a close method. Only the
// containing multi reader needs to be closed by an application.
namespace Splunk
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// The <see cref="ResultsReader"/> abstract class represents the results
    /// reader that returns events from a stream in key/value pairs.
    /// </summary>
    public abstract class ResultsReader : ISearchResults, IDisposable
    {
        /// <summary>
        /// Field name list.
        /// </summary>
        private readonly List<string> fields = new List<string>();

        /// <summary>
        /// Whether the reader is inside a multi reader.
        /// </summary>
        private readonly bool isInMultiReader;
        
        /// <summary>
        /// Whether the results in this reader are a preview. The default value
        /// is false, which results in no result set skipping.
        /// </summary>
        private bool isPreview;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsReader" /> class.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <param name="isInMultiReader">
        /// Whether constructed inside a multi reader.
        /// </param>
        protected ResultsReader(Stream stream, bool isInMultiReader)
        {
            ResponseStream responseStream = stream as ResponseStream;
            if (responseStream != null)
            {
                this.IsExportStream = responseStream.IsExportResult;
            }

            this.isInMultiReader = isInMultiReader;
        }

        /// <summary>
        /// Gets a value indicating whether the stream is 
        /// from the export endpoint.
        /// </summary>
        internal bool IsExportStream { get; private set; }
        
        /// <summary>
        /// Gets all the field names that may appear in each result.
        /// </summary>
        /// <remarks>
        /// Be aware that any given result will contain a subset of these 
        /// fields.
        /// </remarks>
        public virtual ICollection<string> Fields
        {
            get { return this.fields; }
        }

        /// <summary>
        /// Gets a value indicating whether the results in this
        /// reader are a preview from an unfinished search.
        /// </summary>
        /// <remarks>
        /// This property's default value is "false", which results in no 
        /// results set skipping or concatenation.
        /// </remarks>
        public virtual bool IsPreview
        {
            get
            {
                return this.isPreview;
            }

            protected set
            {
                this.isPreview = value;
            }
        }
        /// <summary>
        /// Used by constructors of results readers to obtain the preview flag
        /// and field list, and then skip any previews for export for a
        /// single reader.
        /// </summary>
        internal virtual void FinishInitialization()
        {
            if (this.isInMultiReader)
            {
                return;
            }

            while (true)
            {
                // Stop if no more set is available
                if (!this.AdvanceStreamToNextSet())
                {
                    break;
                }

                // No skipping of result sets if the stream
                // is not from an export endpoint.
                if (!this.IsExportStream)
                {
                    break;
                }

                // Skipping ends at any file results.
                if (!this.isPreview)
                {
                    break;
                }
            }   
        }

        /// <summary>
        /// Releases unmanaged resources.        
        /// </summary>
        public abstract void Dispose();
    
        /// <summary>
        /// Returns an enumerator over the events in the event stream.
        /// </summary>
        /// <remarks>
        /// This property concatenates results across sets when necessary.
        /// </remarks>
        /// <returns>An enumerator of events.</returns>
        public IEnumerator<Event> GetEnumerator()
        {
            while (true)
            {
                foreach (var ret in this.GetEventsFromCurrentSet())
                {
                    yield return ret;
                }

                // We don't concatenate across previews across sets, since each
                // set might be a snapshot at a given time or a summary result
                // with partial data from a reporting search (for example,
                // "count by host"). So if this is a preview,
                // break to end the iteration.
                // Note that isPreview member is used, rather than IsPreview property.
                // We can't use IsPreview property since it will throw
                // if that flag is not available in the result stream 
                // (for example in JSON result stream from Splunk 4.x),
                // rather we want to end the enumeration. isPreview member is
                // default to be false, which means we don't concatenate. It is 
                // the right behavior for JSON result stream from Splunk 4.3.
                if (this.isPreview) 
                {
                    break;
                }

                // If we did not advance to next set, i.e. the end of stream is
                // reached, break to end the iteration.
                if (!this.AdvanceStreamToNextSet())
                {
                    break;
                }

                // We have advanced to the next set. isPreview is for that set.
                // It should not be a preview. Splunk should never return a preview
                // after final results which we might have concatenated together
                // across sets.
                Debug.Assert(
                    !this.isPreview,
                    "Preview result set should never be after a final set.");
            }
        }

        /// <summary>
        /// Returns an enumerator over the events in the event stream.
        /// </summary>
        /// <returns>An enumerator of events.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Gets the current set.
        /// </summary>
        /// <returns>Iterator of events.</returns>
        internal abstract IEnumerable<Event> GetEventsFromCurrentSet();

        /// <summary>
        /// Advances to the next set, skipping remaining event(s) 
        /// if any in the current set.
        /// </summary>
        /// <returns>Returns false if the end is reached.</returns>
        internal abstract bool AdvanceStreamToNextSet();
    }
}

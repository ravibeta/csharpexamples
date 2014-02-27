﻿/*
 * Copyright 2012 Splunk, Inc.
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

namespace Splunk
{
    /// <summary>
    /// The <see cref="JobResultsArgs"/> class contains arguments for getting
    /// job results using the <see cref="Job" /> class.
    /// </summary>
    public class JobResultsArgs : Args
    {
        /// <summary>
        /// Specifies the format for the returned output.
        /// </summary>
        // C# disallows nested classes from having the same name as
        // properties. Use 'Enum' suffix to differentiate.
        public enum OutputModeEnum
        {
            /// <summary>
            /// Returns output in Atom format.
            /// </summary>
            [SplunkEnumValue("atom")]
            Atom,

            /// <summary>
            /// Returns output in CSV format.
            /// </summary>
            [SplunkEnumValue("csv")]
            Csv,

            /// <summary>
            /// Returns output in JSON format.
            /// </summary>
            [SplunkEnumValue("json")]
            Json,

            /// <summary>
            /// Returns output in JSON_COLS format.
            /// </summary>
            [SplunkEnumValue("json_cols")]
            JsonColumns,

            /// <summary>
            /// Returns output in JSON_ROWS format.
            /// </summary>
            [SplunkEnumValue("json_rows")]
            JsonRows,

            /// <summary>
            /// Returns output in raw format.
            /// </summary>
            [SplunkEnumValue("raw")]
            Raw,

            /// <summary>
            /// Returns output in XML format.
            /// </summary>
            [SplunkEnumValue("xml")]
            Xml,
        }

        /* BEGIN AUTOGENERATED CODE */

        /// <summary>
        /// Sets the maximum number of results to return.
        /// </summary>
        public new int Count
        {
            set { this["count"] = value; }
        }

        /// <summary>
        /// Sets a list of fields to return for the event set.
        /// </summary>
        public string[] FieldList
        {
            set { this["f"] = value; }
        }

        /// <summary>
        /// Specifies the index of the first result (inclusive) from which to begin returning 
        /// data. This value is 0-indexed.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Starting in Splunk 4.1, negative offsets are allowed and are added to the count to 
        /// compute the absolute offset (for example, offset=-1 is the last available 
        /// offset). Offsets in the results are always absolute and never negative. 
        /// </para>
        /// <para>
        /// This property's default value is "0".
        /// </para>
        /// </remarks>
        public int Offset
        {
            set { this["offset"] = value; }
        }

        /// <summary>
        /// Sets the format of the output.
        /// </summary>
        public OutputModeEnum OutputMode
        {
            set { this["output_mode"] = value.GetSplunkEnumValue(); }
        }

        /// <summary>
        /// Sets the post-processing search to apply to results.
        /// </summary>
        public string Search
        {
            set { this["search"] = value; }
        }

        /* END AUTOGENERATED CODE */
    }
}
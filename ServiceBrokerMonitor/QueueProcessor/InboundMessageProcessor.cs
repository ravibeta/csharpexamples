//===============================================================================
// Copyright © 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
//
//  Modified by Ravi Rajamani to integrate with Splunk
//
//===============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;
using System.Data.SqlClient;
using Splunk.ModularInputs;

namespace QueueProcessor
{
  class InboundMessageProcessor
  {
    public static void ProcessMessage(byte[] message)
    {
      Trace.WriteLine("InboundMessageProcessor Recieved Message");
      using (var writer = new EventStreamWriter())
      {
          var varName = "ServiceBrokerData";
          writer.Write(
            new EventElement
            {
                Source = varName,
                Data = Encoding.Default.GetString(message),
            });
      }
      return;
    }

    public static void SaveFailedMessage(byte[] message, SqlConnection con, Exception errorInfo)
    {
      Trace.WriteLine("InboundMessageProcessor Recieved Failed Message");
      using (var writer = new EventStreamWriter())
      {
          var varName = "ServiceBrokerData";
          writer.Write(
            new EventElement
            {
                Source = varName,
                Data = Encoding.Default.GetString(message),
            });
      }
      return;
    }
  }
}

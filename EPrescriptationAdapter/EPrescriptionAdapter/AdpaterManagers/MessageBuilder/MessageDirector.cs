using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdpaterManagers.MessageBuilder
{
    /// <summary>
    ///   Class:          ScriptDirector
    ///   Created by:     Waqas Baber 
    ///   Description:    Using Builder pattern : Script Director Consutructs the builders classes
    ///   Notes:          
    ///   Copyright 2011 Zigron Inc. All rights reserved  
    /// </summary>
    public class MessageDirector
    {
        /// <summary>
        /// Building parts of the Desired script  
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="choiceList"></param>
        public void Construct(MessageBuilder builder, object[] choiceList)
        {
          builder.BuildScript(choiceList);
        }
    }
    
}

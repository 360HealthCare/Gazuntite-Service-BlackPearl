using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdpaterManagers.MessageBuilder
{
    /// <summary>
    ///   Class:          MessageBuilder
    ///   Created by:     Waqas Baber 
    ///   Description:    abstract Builder class for building custom IVR Scripts 
    ///   Notes:          
    ///   Copyright 2011 Zigron Inc. All rights reserved  
    /// </summary>
    public abstract class MessageBuilder
    {
        /// <summary>
        /// Build any custom sript based on dto list
        /// </summary>
        /// <param name="choiceList"></param>
        public abstract void BuildScript(object dto);
        /// <summary>
        /// return any custome built script
        /// </summary>
        /// <returns></returns>
        public abstract Message GetContent();
    }
   
}

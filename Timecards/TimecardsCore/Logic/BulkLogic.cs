﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimecardsCore.Interfaces;

namespace TimecardsCore.Logic
{
    public class BulkLogic
    {
        private readonly IFactory _factory;

        public BulkLogic(IFactory factory)
        {
            _factory = factory;
        }

        public string Export(DateTime? StartDate, DateTime? EndDate, DataFormat format)
        {
            var result = new StringBuilder();

            // retrieve the timecards to be exported
            //TODO: finish
            //TODO: transform the data

            return result.ToString();
        }

        public void Import(string path, DataFormat format)
        {
            //TODO: read in the data
            //TODO: transform the data
            //TODO: store the data, raising an event indicating progress
        }

        public enum DataFormat
        {
            [Description("Comma-delimited text")]
            CommaDelimitedText,

            [Description("Tab-delimited text")]
            TabDelimitedText,

            [Description("JSON")]
            JSON,

            [Description("XML")]
            XML
        }
    }
}

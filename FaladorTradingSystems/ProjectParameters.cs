using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaladorTradingSystems
{
    /// <summary>
    /// class to store basic (constant) parameters
    /// for the project - eg location of data as
    /// not currently in database
    /// </summary>
    
    internal static class ProjectParameters
    {
        internal static string DataFileLocation =>
            @"C:\Users\joelo\code\runescape\data_cache\full_data.csv";
    }
}

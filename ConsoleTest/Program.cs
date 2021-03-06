﻿using Jdart.Swiffy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var swf = File.ReadAllBytes("1.swf");

            var swiffyClient = new SwiffyClient();
         
            var result = swiffyClient.ConvertToHtml5Async(swf).Result;
            var result2 = swiffyClient.ConvertToHtml5Async(swf).Result;

            File.WriteAllBytes("sample.html", Encoding.UTF8.GetBytes(result));
        }
    }
}

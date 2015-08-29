# swiffy

Sample:

var swf = File.ReadAllBytes("sample.swf");

var swiffyClient = new SwiffyClient();
         
string html5page = await swiffyClient.ConvertToHtml5Async(swf);

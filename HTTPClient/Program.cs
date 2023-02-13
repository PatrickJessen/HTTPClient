using System.Net;

// Get html source from url
static string Website(string url)
{

    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
    myRequest.Method = "GET";
    WebResponse myResponse = myRequest.GetResponse();
    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
    string result = sr.ReadToEnd();
    sr.Close();
    myResponse.Close();

    return result;
}

// Removes tags specified in the tags array
// It was too boring to use regex so i tried to make my own
static string RemoveTags(string url, string[] tags)
{
    string temp = url;
    for (int i = 0; i < tags.Length; i++)
    {
        for (int j = 0; j < temp.Length; j++)
        {
            string tag = "";
            int counter = 0;
            if (temp[j + counter] == '<')
            {
                while (temp[j + counter] != '>')
                {
                    tag += temp[j + counter];
                    counter++;
                }
                tag += temp[j + counter];
                if (tag == tags[i])
                {
                    temp = temp.Remove(j, counter + 1);
                }
            }
        }
    }
    return temp;
}

// tags to be removed in the html file string
string[] tags = { "<div>", "</div>", "<html>", "</html>", "<body>", "</body>", "</script>", "<script>" };
string html = Website("https://www.codeproject.com/Questions/204778/Get-HTML-code-from-a-website-C");
Console.WriteLine(RemoveTags(html, tags));
Console.Read();
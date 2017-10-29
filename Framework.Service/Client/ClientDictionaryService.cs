using Framework.Repository.RepositorySpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Framework.Model;
using System.Net;
using System.IO;

namespace Framework.Service.Client
{
    public interface IClientDictionaryService
    {
        Task<Dict> startCrawlerOxford(string voca);
        Task startCrawlerTraCau(string voca);
        void GoogleTranslator(string voca);
    }
    class ClientDictionaryService : IClientDictionaryService
    {
        ICodeRepository _codeRepository;
        Dict dictResult;
        DictExample example;
        public ClientDictionaryService(
            )
        {
            dictResult = new Dict();
            dictResult.m_UseCaseOfVoca = new List<DictExample>();
            example = new DictExample();
            example.m_Sentence = new List<string>();
        }
        //oxford dictionary
        public async Task <Dict> startCrawlerOxford(string voca)
        {
            var url = "https://www.oxfordlearnersdictionaries.com/definition/english/word";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            List<HtmlNode> divs = htmlDocument.DocumentNode.Descendants("ol").Where(node => node.GetAttributeValue("class", "").Equals("h-g")).ToList();
            GetPronAndSoundOfOxford(divs);
            var temps = divs.FirstOrDefault().Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("sn-gs")).ToList();

            foreach (var tmp in temps)
            {
                GetExampleListOxFord(tmp);
            }
            return dictResult;
        }
        private string GetPronAndSoundOfOxford(List<HtmlNode> ListNode)
        {
            var temp = ListNode.FirstOrDefault().Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("top-container")).ToList().FirstOrDefault();
            temp = temp.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("top-g")).ToList().FirstOrDefault();
            temp = temp.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("pron-gs ei-g")).ToList().FirstOrDefault();
            temp = temp.Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("pron-g")).ToList().FirstOrDefault();
            string result = temp.Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("phon")).ToList().FirstOrDefault().InnerText;
            string urlSound = temp.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("sound audio_play_button pron-uk icon-audio")).ToList().FirstOrDefault().ChildAttributes("data-src-mp3").FirstOrDefault().Value;
            try
            {
                dictResult.m_UrlSound = urlSound;
                dictResult.m_Pro = result;
            }catch(Exception e)
            {
                string error = e.ToString();
            }
            return null;
        }
        public void GetExampleListOxFord(HtmlNode Node)
        {
            try
            {
                //var temp = Node.Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("sn-gs")).ToList();
                var temp = Node.Descendants("li").Where(node => node.GetAttributeValue("class", "").Equals("sn-g")).ToList().FirstOrDefault();

                var exampleList = temp.Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("def")).FirstOrDefault();
                //Set value for usecase
                example.m_UseCase = exampleList.InnerText;
                //Get a example for usecase
                List<HtmlNode> exampleList2 = temp.Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("x-g")).ToList();
                foreach (var exam in exampleList2)
                {
                    example.m_Sentence.Add(exam.InnerText);
                }
                dictResult.m_UseCaseOfVoca.Add(example);
            }
            catch(Exception e)
            {
                return;
            }
            //public 
        }
        //tracau.vn
        public async Task startCrawlerTraCau(string voca)
        {
            var url = "https://api.tracau.vn/WBBcwnwQpV89/s/without%20you/ev%7Cdi%7Ccn%7Caa/en";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            List<HtmlNode> divs = htmlDocument.DocumentNode.Descendants("article").Where(node => node.GetAttributeValue("class", "").Equals("web-article web-article--mini")).ToList();
            foreach (var tmp in divs)
            {
               // GetExampleListOxFord(tmp);
            }
        }
        public void GoogleTranslator(string voca)
        {
            var url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl=vi&hl=en-US&dt=t&dt=bd&dj=1&source=bubble&tk=908123.908123&q=what your name";


            var webRequest = WebRequest.Create(@"https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl=vi&hl=en-US&dt=t&dt=bd&dj=1&source=bubble&tk=908123.908123&q=what your name");

            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                var strContent = reader.ReadToEnd();
                string i = (string)strContent;
            }
            var textFromFile = (new WebClient()).DownloadString(url);
           
        }
    }
}

using Framework.Controllers;
using Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.ViewModels
{

    public class DictionariesViewModel : LayoutViewModel, IRef<DictionaryController>
    {
        public DictionariesViewModel()
        {
            m_UseCaseOfVoca = new List<DictExample>();

        }
        public string m_Voca { get; set;}
        public string m_Pro { get; set;}
        public string m_UrlSound {get; set;}
        public List<DictExample> m_UseCaseOfVoca;
    }

    public class OldWordsViewModel : LayoutViewModel, IRef<DictionaryController>
    {

    }
}
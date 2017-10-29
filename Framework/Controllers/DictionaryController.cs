using Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Service.Admin;
using Framework.Common;
using System.Threading;
using System.Web.UI;
using System.Drawing;
using System.IO;
using System.Drawing.Text;
using Framework.ViewModels;
using Framework.Service.Client;
using Framework.Model;

namespace Framework.Controllers
{

    public class DictionaryController : LayoutController
    {
        IClientDictionaryService _clientDictionaryService;
        public DictionaryController(ILayoutService layoutService,
            IClientDictionaryService clientDictionaryService
            )
            : base(layoutService)
        {
            _clientDictionaryService = clientDictionaryService;
        }


        DictionariesViewModel DictionariesViewModel
        {
            get
            {
                return (DictionariesViewModel)_viewModel;
            }
        }

        OldWordsViewModel OldWordsViewModel
        {
            get
            {
                return (OldWordsViewModel)_viewModel;
            }
        }

        public ActionResult Index(string option)
        {
            if (option != null)
            {
                switch (option.ToLower())
                {
                    case "dictionaries":
                        {
                            _viewModel = new DictionariesViewModel();
                            CreateLayoutView("Từ điển");
                            break;
                        }
                    case "oldwords":
                        {
                            _viewModel = new OldWordsViewModel();
                            CreateLayoutView("Từ đã tra");
                            break;
                        }
                    default:
                        {
                            _viewModel = new DictionariesViewModel();
                            CreateLayoutView("Từ điển");
                            break;
                        }
                }
            }
            else
            {
                _viewModel = new DictionariesViewModel();
                CreateLayoutView("Từ điển");
            }

            return View(_viewModel);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Dictionaries(string keyword)
        {
            _viewModel = new DictionariesViewModel();
             Dict dict = await _clientDictionaryService.startCrawlerOxford(keyword);
            //_clientDictionaryService.GoogleTranslator("");
            //  await _clientDictionaryService.startCrawlerTraCau("");
            DictionariesViewModel.m_Pro = dict.m_Pro;
            DictionariesViewModel.m_UrlSound = dict.m_UrlSound;
            DictionariesViewModel.m_Voca = dict.m_Voca;
            DictionariesViewModel.m_UseCaseOfVoca = dict.m_UseCaseOfVoca;
            return PartialView("_Dictionaries", DictionariesViewModel);
        }

        [HttpGet]
        public ActionResult OldWords()
        {
            _viewModel = new OldWordsViewModel();

            return PartialView("_OldWords", OldWordsViewModel);
        }
    }
}

using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.Models;
using System.Data.Entity;
using Tender.Models.PageModel;
using System.Collections.Generic;

namespace Tender.Controllers
{
    public class HomeController : Controller
    {
        private TenderContext db = new TenderContext();

        public int PageSize = 2;

        [HttpGet]
        public ActionResult Index(int page = 1, string text = "", int Sponsor = 0, int Type = 0, string StartData = "01/01/2018", string EndData = "01/12/2018", string rate = "", string time = "", int Category = 0)
        {
            Session["page"] = page;
            Session["text"] = text;
            Session["Sponsor"] = Sponsor;
            Session["Type"] = Type;
            Session["Rate"] = rate;
            Session["Time"] = time;
            Session["Category"] = Category;
            var text_search = Session["text"].ToString();
            var se_sponsor = Convert.ToInt32(Session["Sponsor"]);
            var se_type = Convert.ToInt32(Session["Type"]);
            var se_category = Convert.ToInt32(Session["Category"]);

            DateTime start = Convert.ToDateTime(StartData);
            DateTime end = Convert.ToDateTime(EndData);

            if (se_sponsor != 0)
            {
                var name_sponsor = db.Cards.Where(x => x.Id == se_sponsor).FirstOrDefault();
                var count_filter = db.Cards
                    .Include(x => x.Сurrency)
                    .Where(x => x.Sponsor == name_sponsor.Sponsor && x.TypeId == se_type && x.PublicationDate >= start && x.PublicationDate <= end).Count();

                if (count_filter != 0)
                {
                    TenderListViewModel filter = new TenderListViewModel
                    {
                        Cards = db.Cards
                        .Include(x => x.Сurrency)
                        .Where(x => x.Sponsor == name_sponsor.Sponsor && x.TypeId == se_type && x.PublicationDate >= start && x.PublicationDate <= end)
                        .OrderBy(x => x.Id)
                        .Skip((Convert.ToInt32(Session["page"]) - 1) * PageSize)
                        .Take(PageSize),
                        PagingInfo = new PagingInfo
                        {
                            CurrentPage = Convert.ToInt32(Session["page"]),
                            ItemsPerPage = PageSize,
                            TotalItems = count_filter
                        }
                    };

                    return View(filter);
                }
                else
                {
                    ViewBag.Search = "Поиск не дал результатов...";
                    return View();
                }
            }
            else if (Session["Time"].ToString() == "asc")
            {

                TenderListViewModel result = new TenderListViewModel
                {
                    Cards = db.Cards.Include(x => x.Сurrency)
                    .OrderBy(x => x.Id)
                    .Skip((Convert.ToInt32(Session["page"]) - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = Convert.ToInt32(Session["page"]),
                        ItemsPerPage = PageSize,
                        TotalItems = db.Cards.Count()
                    }
                };

                return View(result);

            }
            else if (Session["Time"].ToString() == "desc")
            {
                TenderListViewModel result = new TenderListViewModel
                {
                    Cards = db.Cards.Include(x => x.Сurrency)
                    .OrderByDescending(x => x.Id)
                    .Skip((Convert.ToInt32(Session["page"]) - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = Convert.ToInt32(Session["page"]),
                        ItemsPerPage = PageSize,
                        TotalItems = db.Cards.Count()
                    }
                };
                return View(result);
            }
            else if (Session["Rate"].ToString() == "more")
            {
                TenderListViewModel result = new TenderListViewModel
                {
                    Cards = db.Cards.Include(x => x.Сurrency)
                    .OrderByDescending(x => x.Rate)
                    .Skip((Convert.ToInt32(Session["page"]) - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = Convert.ToInt32(Session["page"]),
                        ItemsPerPage = PageSize,
                        TotalItems = db.Cards.Count()
                    }
                };

                return View(result);
            }
            else if (Session["Rate"].ToString() == "less")
            {
                TenderListViewModel result = new TenderListViewModel
                {
                    Cards = db.Cards.Include(x => x.Сurrency)
                    .OrderBy(x => x.Rate)
                    .Skip((Convert.ToInt32(Session["page"]) - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = Convert.ToInt32(Session["page"]),
                        ItemsPerPage = PageSize,
                        TotalItems = db.Cards.Count()
                    }
                };

                return View(result);
            }
            else if (se_category != 0)
            {
                var count = db.Cards.Include(x => x.Сurrency).Where(x => x.СategoryId == se_category).Count();
                TenderListViewModel result = new TenderListViewModel
                {
                    Cards = db.Cards.Include(x => x.Сurrency).Where(x => x.СategoryId == se_category)
                    .OrderBy(x => x.Id)
                    .Skip((Convert.ToInt32(Session["page"]) - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = Convert.ToInt32(Session["page"]),
                        ItemsPerPage = PageSize,
                        TotalItems = count
                    }
                };

                return View(result);
            }
            else
            {
                if (text_search == "")
                {
                    TenderListViewModel result = new TenderListViewModel
                    {
                        Cards = db.Cards.Include(x => x.Сurrency)
                        .OrderBy(x => x.Id)
                        .Skip((Convert.ToInt32(Session["page"]) - 1) * PageSize)
                        .Take(PageSize),
                        PagingInfo = new PagingInfo
                        {
                            CurrentPage = Convert.ToInt32(Session["page"]),
                            ItemsPerPage = PageSize,
                            TotalItems = db.Cards.Count()
                        }
                    };

                    return View(result);
                }
                else
                {

                    var count_Subject = db.Cards.Include(x => x.Сurrency).Where(x => x.Subject.StartsWith(text_search)).Count();
                    var count_Description = db.Cards.Include(x => x.Сurrency).Where(x => x.Description.StartsWith(text_search)).Count();

                    if (count_Subject != 0)
                    {

                        TenderListViewModel result = new TenderListViewModel
                        {
                            Cards = db.Cards.Include(x => x.Сurrency).Where(x => x.Subject.StartsWith(text_search))
                            .OrderBy(x => x.Id)
                            .Skip((Convert.ToInt32(Session["page"]) - 1) * PageSize)
                            .Take(PageSize),
                            PagingInfo = new PagingInfo
                            {
                                CurrentPage = Convert.ToInt32(Session["page"]),
                                ItemsPerPage = PageSize,
                                TotalItems = count_Subject
                            }
                        };

                        return View(result);
                    }
                    else if (count_Description != 0)
                    {
                        TenderListViewModel result = new TenderListViewModel
                        {
                            Cards = db.Cards.Include(x => x.Сurrency).Where(x => x.Description.StartsWith(text_search))
                            .OrderBy(x => x.Id)
                            .Skip((Convert.ToInt32(Session["page"]) - 1) * PageSize)
                            .Take(PageSize),
                            PagingInfo = new PagingInfo
                            {
                                CurrentPage = Convert.ToInt32(Session["page"]),
                                ItemsPerPage = PageSize,
                                TotalItems = count_Subject
                            }
                        };

                        return View(result);
                    }
                    else
                    {
                        ViewBag.Search = "Поиск не дал результатов...";
                        return View();
                    }
                }
            }
        }

        [ChildActionOnly]
        public ActionResult Filter()
        {
            SelectList sponsor = new SelectList(db.Cards, "Id", "Sponsor");
            SelectList types = new SelectList(db.Types, "Id", "NameType");

            ViewBag.sponsor = sponsor;
            ViewBag.types = types;

            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Sort()
        {
            return PartialView(db.Categories);
        }

        public ActionResult Card(int id)
        {
            Card card = db.Cards
                .Include(x => x.Type)
                .Include(x => x.Сategory)
                .Include(x => x.Сurrency)
                .Where(x => x.Id == id)
                .FirstOrDefault();
            return View(card);
        }

        public ActionResult AddTender()
        {
            SelectList types = new SelectList(db.Types, "Id", "NameType");
            SelectList categories = new SelectList(db.Categories, "Id", "NameCategory");
            SelectList currencies = new SelectList(db.Currencies, "Id", "NameСurrency");

            ViewBag.Types = types;
            ViewBag.Categories = categories;
            ViewBag.Currencies = currencies;

            return View();
        }

        [HttpPost]
        public ActionResult AddTender(Card card)
        {
            db.Cards.Add(card);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
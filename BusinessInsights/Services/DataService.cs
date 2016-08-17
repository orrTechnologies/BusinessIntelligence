//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Microsoft.Owin.Security.Facebook;
//using BusinessInsights.Data;
//using BusinessInsights.Data.Models;

//namespace BusinessInsights.Services
//{
//    public class DataService
//    {
//        private readonly FacebookContext _context;

//        public DataService(FacebookContext context)
//        {
//            _context = context;
//        }

//        public void CreatePage(Page page)
//        {
//            _context.Pages.Add(page);
//            _context.SaveChanges();
//        }
//        /// <summary>
//        /// Returns page from database if exist
//        /// </summary>
//        /// <param name="id">Facebook page Id</param>
//        /// <returns>Facebook Page</returns>
//        public Page FindById(int id)
//        {
//           var page = _context.Pages.FirstOrDefault(p => p.PageId == id);
//           return page;
//        }
//    }
//}
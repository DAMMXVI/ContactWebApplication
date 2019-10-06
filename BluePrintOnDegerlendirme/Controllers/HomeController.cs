using ContactWebApplication.Models;
using ContactWebApplication.Models.dbManager;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ContactWebApplication.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Home
        public ActionResult Index() {   //Her run edildiğinde örnek bir kayıt ekle.
            AddRecord();
            db.SaveChanges();
            return View();
        }

        public ActionResult Change(String LanguageAbbrevation)
        {
            if (LanguageAbbrevation != null)    //Dil seçimi yapılmış ise CurrentCulture  ile tarih zaman gibi değişkenleri, CurrentUICulture ilede kullanıcı arayüz dil seçimini değiştir.
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }
            HttpCookie cookike = new HttpCookie("Language");    //Çerezi oluştur ve dil tercihini ekle.
            cookike.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookike);
            return Redirect(Request.UrlReferrer.AbsoluteUri);   //Dil tercihi değiştirildiğindeki mevcut sayfaya tekrar yönlendir.
        }

        public void AddRecord() //Örnek kayıt ekleme metodu
        {
            Contact contact = new Contact();
            contact.fullName = "Alper Deniz";
            contact.phoneNumber = "05346447545";
            contact.address = "Mehmet Akif Ersoy Mah. 5316.sokak No:1";
            contact.note = "Note";
            contact.dateAdded = DateTime.Now;
            db.Contacts.Add(contact);
        }
    }  
}
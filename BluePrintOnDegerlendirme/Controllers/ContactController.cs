using ContactWebApplication.Models;
using ContactWebApplication.Models.dbManager;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ContactWebApplication.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        DatabaseContext db = new DatabaseContext();
        public ActionResult AddContact()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddContact(Contact model)
        {
            if (ModelState.IsValid) //Model geçerli bir şekilde doldurulmuş ise veritabanına kaydet ve ekrana başarılı mesajı yazdır.(method ile)
            {
                model.dateAdded = DateTime.Now;
                db.Contacts.Add(model);
                db.SaveChanges();
                showMessageGoUrl(Resources.Resource.AddSuccessMessage, "ListContact");
            }
            return View();
        }

        public ActionResult ListContact(int? page)  //Listenin ilk sayfası haricinde bir sayfa açılmak istendiğinde page değişkenine sayfa numarasını al.
        {
            var model = db.Contacts.ToList().ToPagedList(page ?? 1, 10);    //eğer (page değişkeni varsa o sayfayı etkinkıl yoksa ilk sayfayı etkin kıl) ve 10 adet kayıt göster
            return View(model);
        }

        public ActionResult DelContact(int? id)
        {
            if (id != null)
            {
                Contact delRecord = db.Contacts.Find(id);
                if (delRecord != null)  //Silinecek kayıt bulunduysa veritabanından kaldır ve ilk sayfayı etkin kılarak her sayfada 10 kayıt ile listeleme sayfasına dön.
                {
                    db.Contacts.Remove(delRecord);
                    db.SaveChanges();
                    var model = db.Contacts.ToList().ToPagedList(1, 10);
                    return View("ListContact", model);
                }
                else
                    return HttpNotFound();
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditContact(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Contact editRecord = db.Contacts.Find(id);
            if (editRecord == null)
                return HttpNotFound();

            return View(editRecord);

        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditContact(Contact contacts)
        {
            if (ModelState.IsValid) //Model geçerli bir şekilde doldurulmuş ise veritabanından güncelle ve ilk sayfayı etkin kılarak her sayfada 10 kayıt ile listeleme sayfasına dön.
            {
                contacts.dateEdited = DateTime.Now;
                db.Entry(contacts).State = EntityState.Modified;
                db.SaveChanges();
                var modell = db.Contacts.ToList().ToPagedList(1, 10);
                return View("ListContact", modell);
            }
            return View(contacts);

        }

        [HttpPost]
        public ActionResult DetailsContact(int? id)
        {
            if (id != null)
            {
                Contact detRecord = db.Contacts.Find(id);
                if (detRecord != null)  //Url'de gelen id null değil ve o id'e veritabanında kayıtlı bir kayıt varsa parçalı view'e o kayıtı döndür.
                    return PartialView(detRecord);
                else
                    return HttpNotFound();
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        public JsonResult GetSearchingData(string SearchBy, string SearchValue)
        {
            //Aranacak parametrelere göre aranacak değer doldurulmuş ise doldurulan değer ile başlayan bir kayıt varsa ilk sayfadan itibaren her sayfada 10 kayıt olacak şekilde döndür. Herhangi bir değer girilmemişse bütün kayıtları döndür.
            if (SearchBy == "Name")
            {
                var model = db.Contacts.Where(m => m.fullName.StartsWith(SearchValue) || SearchValue == null).ToList().ToPagedList(1,10);
                return Json(model, JsonRequestBehavior.AllowGet);

            }
            else if (SearchBy == "PhoneNumber")
            {
                var model = db.Contacts.Where(m => m.phoneNumber.StartsWith(SearchValue) || SearchValue == null).ToList().ToPagedList(1, 10);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var model = db.Contacts.Where(m => m.address.StartsWith(SearchValue) || SearchValue == null).ToList().ToPagedList(1, 10);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        public void showMessageGoUrl(string message, string goUrl)  //Ekrana mesaj yaz ve belirtilen adrese git
        {
            Response.Write($@"<script language='javascript'>
                            alert('{message}');
                            window.location='{goUrl}'
                            </script>");
        }

    }
}
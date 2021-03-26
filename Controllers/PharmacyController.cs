using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L3_DAVH_AFPE.Models;
using L3_DAVH_AFPE.Models.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;


namespace L3_DAVH_AFPE.Controllers
{
    public class PharmacyController : Controller
    {
        public string pathito = "";
        private readonly IHostingEnvironment hostingEnvironment;
        public PharmacyController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        // GET: PharmacyController
        public ActionResult Index()
        {
            return View(Singleton.Instance.orders);
        }

        // GET: PharmacyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PharmacyController/Create
        public ActionResult Create()
        {
            while (Singleton.Instance.options.Length < 0)
            {
                Singleton.Instance.options.Delete(0);
            }
            Singleton.Instance.Traverse(Singleton.Instance.guide.Root);
            return View();
        }

        // POST: PharmacyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {

            try
            {
                var newOrder = new Cart
                {
                    clientName = collection["clientName"],
                    NIT = collection["NIT"],
                    address = collection["address"],
                    product = collection["product"]
                };
                string[] a = collection["product"].ToString().Split('-');
                string name = "";
                for (int i = 0; i < a.Length - 1; i++)
                {
                    name += a[i].Trim();
                }
                int b = a.Length - 1;
                newOrder.amount = double.Parse(a[b].Replace('$', ' ').Trim());
                Singleton.Instance.orders.InsertAtEnd(newOrder);
                Drug obj = new Drug { name = name, numberline = 0 };
                int idx = Singleton.Instance.guide.Find(obj, Singleton.Instance.guide.Root).value.numberline;
                PharmacyModel x = Singleton.Instance.inventory.Get(idx);
                x.Quantity--;
                if (x.Quantity == 0)
                {
                    Singleton.Instance.guide.Delete(Singleton.Instance.guide.Root, obj);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: PharmacyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PharmacyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PharmacyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PharmacyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Import()
        {
            if (!Singleton.Instance.fileUpload)
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public ActionResult ViewTree()
        {
            Singleton.Instance.PrintTree(Singleton.Instance.guide.Root);
            return View();
        }
        public ActionResult DownloadFile()
        {
            StreamWriter file = new StreamWriter("Guide.txt", true);
            file.Write(Singleton.Instance.PrintTree(Singleton.Instance.guide.Root));
            file.Close();
            byte[] fileBytes = System.IO.File.ReadAllBytes("Guide.txt");
            string fileName = "Guide.txt";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public ActionResult Download()
        {
            return File(pathito, Singleton.Instance.PrintTree(Singleton.Instance.guide.Root), "reoprt.txt");
        }

        [HttpPost]
        public ActionResult Import(FileModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                string filePath = null;
                if (model.File != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    pathito = filePath;
                    model.File.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                TextReader txtrdr = new StreamReader(model.File.OpenReadStream());
                TextFieldParser txtfldprsr = new TextFieldParser(txtrdr);
                txtfldprsr.SetDelimiters(new string[] { "," });
                txtfldprsr.HasFieldsEnclosedInQuotes = true;

                string[] Drugss;
                while (!txtfldprsr.EndOfData)
                {
                    try
                    {
                        Drugss = txtfldprsr.ReadFields();
                        var newDrug = new PharmacyModel
                        {
                            Id = int.Parse(Drugss[0]),
                            Name = Drugss[1].ToString(),
                            Description = Drugss[2].ToString(),
                            Production_Factory = Drugss[3].ToString(),
                            Price = double.Parse(Drugss[4].Substring(1)),
                            Quantity = int.Parse(Drugss[5])
                        };
                        int cant = 0;
                        while (Singleton.Instance.inventory.Find(newDrug) != null)
                        {
                            newDrug.Name += "-" + ++cant;
                        }
                        Singleton.Instance.inventory.InsertAtEnd(newDrug);
                        if (newDrug.Quantity > 0)
                        {
                            int cont = 0;
                            if (Drugss[0] == "104") 
                            { 
                            }
                            while (Singleton.Instance.guide.Find(new Drug { name = Drugss[1], numberline = int.Parse(Drugss[0]) }, Singleton.Instance.guide.Root) != null)
                            {
                                Drugss[1] += "-" + ++cont;
                            }
                            Singleton.Instance.guide.Insert(new Drug { name = Drugss[1], numberline = int.Parse(Drugss[0]) }, Singleton.Instance.guide.Root);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }

            }
            return RedirectToAction(nameof(Index));
        }


    }
}

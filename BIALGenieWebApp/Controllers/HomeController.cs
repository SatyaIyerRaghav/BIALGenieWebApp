using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using BIALGenieWebApp.Models;

namespace BIALGenieWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        passengersEntities1 db = new passengersEntities1();

        public ActionResult Homepage()
        {
            return View();
        }
             
        public ActionResult FlightDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FlightDetails(FlightDetail flightdt)
        {
            try{
                var pno = Session["pno"];
                FlightDetail ff = new FlightDetail();
                ff.ArrivalAirline = flightdt.ArrivalAirline;
                ff.ArrivalDate = flightdt.ArrivalDate;
                ff.ArrivalFlightNo = flightdt.ArrivalFlightNo;
                ff.ArrivalTime = flightdt.ArrivalTime;
                ff.FlightType = flightdt.FlightType;
                ff.Origin = flightdt.Origin;
                ff.PNRNumber = flightdt.PNRNumber;
                ff.PassportNumber = pno.ToString();

                db.FlightDetails.Add(ff);
                db.SaveChanges();
                ViewData["lblmsg"] = "Flight Details Saved Successfully";
            }
            catch(Exception ex)
            {
                ViewData["lblmsg"] = ex.ToString();
            }
            return View();
        }

        [HttpGet]
        public ActionResult PassengerDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PassengerDetails(PassengerDetail passdt)
        {
            try
            {

                db.PassengerDetails.Add(passdt);
                db.SaveChanges();
                ViewData["lblmsg"] = "Flight Details Saved Successfully";
                Session["pno"] = passdt.PassportNumber;
                return RedirectToAction("PassengerDetailsList");
            }
            catch (Exception ex)
            {
                ViewData["lblmsg"] = ex.ToString();
            }
            return View();
        }

        public ActionResult PassengerDetailsList()
        {
            var passlist = db.PassengerDetails.ToList();
            return View(passlist.ToList());
        }

        [HttpGet]
        public ActionResult EditPassengerDetailsList(string id)
        {
            var passlist = db.PassengerDetails.Where(i => i.PassportNumber == id.ToString()).FirstOrDefault();
            Session["pno"] = passlist.PassportNumber;
            return View(passlist);
        }

        [HttpPost]
        public ActionResult EditPassengerDetailsList(PassengerDetail passdt)
        {
            try
            {
                passengersEntities1 db = new passengersEntities1();
                db.PassengerDetails.Attach(passdt);
                db.Entry(passdt).State = EntityState.Modified;
                db.SaveChanges();
                ViewData["lblmsg"] = "Passenger Details Updated Successfully";
                return RedirectToAction("PassengerDetailsList");
            }
            catch (Exception ex)
            {
                ViewData["lblmsg"] = ex.ToString();
            }
            return View();
        }

        [HttpGet]
        public ActionResult DeletePassengerDetailsList(string id)
        {
            try
            {
                var passlist = db.PassengerDetails.Where(i => i.PassportNumber == id.ToString()).FirstOrDefault();
                db.Entry(passlist).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("PassengerDetailsList");
            }
            catch (Exception ex)
            {
                ViewData["lblmsg"] = ex.ToString();
            }
            return View();
        }

        public ActionResult GymServicesDetails()
        {
            return View();
        }

        public ActionResult SpaServicesDetails()
        {
            return View();
        }

        public ActionResult SwimServicesDetails()
        {
            return View();
        }

        public ActionResult yogaServicesDetails()
        {
            return View();
        }

        public ActionResult gameServicesDetails()
        {
            return View();
        }
       
        public ActionResult OnlineBooking()
        {
            
            var guestlist = new SelectList(db.PassengerDetails.Select(i => i.GuestName).ToList());
            ViewData["guest"] = guestlist;
            return View();
        }

        [HttpPost]
        public ActionResult OnlineBooking(BookingDetail bookdt)
        {
            var guestlist = new SelectList(db.PassengerDetails.Select(i => i.GuestName).ToList());
            ViewData["guest"] = guestlist;

            var pno = bookdt.PassportNumber;
            var gname = bookdt.GuestName;
            var pass = db.PassengerDetails.Where(i => i.PassportNumber == pno && i.GuestName == gname).FirstOrDefault();
            
            if (pass != null)
            {
                var book = db.BookingDetails.Where(i => i.PassportNumber == pno && i.GuestName == gname).FirstOrDefault();

                if (book != null)
                {
                    try
                    {
                        book.GuestName = bookdt.GuestName;
                        book.PassportNumber = bookdt.PassportNumber;
                        book.ServiceOpted = bookdt.ServiceOpted;
                        book.Status = bookdt.Status;
                        book.TimeAt = bookdt.TimeAt;
                        book.AmountPaid = bookdt.AmountPaid;
                        book.DateBooked = bookdt.DateBooked;
                        book.DateOn = bookdt.DateOn;
                        db.BookingDetails.Attach(book);
                        db.Entry(book).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewData["lblmsg"] = "Booking Details Saved Successfully";
                        ViewData["statusinfo"] = "Verification Successfull";
                        Session["service"] = bookdt.PassportNumber;
                    }
                    catch (Exception ex)
                    {
                        ViewData["lblmsg"] = ex.ToString();
                    }
                }
                else
                {
                    try
                    {
                        db.BookingDetails.Add(bookdt);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewData["lblmsg"] = ex.ToString();
                    }
                }
            }
            else
            {
                ViewData["statusinfo"] = "Not a Verified Passport Number.Please Check again.";
            }

            return View();
        }

        [HttpGet]
        public ActionResult ChildView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChildView(string duration,string payment,string pno, string gname)
        {
            passengersEntities1 db = new passengersEntities1();
            if( duration == "30 min(Gymming)")
            {
                string amt = "Rs.400 (Including Taxes)";
                gymservice gym = new gymservice();
                gym.PassportNumber = pno;
                gym.GuestName = gname;
                gym.duration = duration;
                gym.payment = payment;
                gym.amount = amt;
                db.gymservices.Add(gym);
                db.SaveChanges();
               
            }
            if (duration == "1 Hour (Gymming)")
            {
                string amt = "Rs.600 (Including Taxes)";
                gymservice gym = new gymservice();
                gym.PassportNumber = pno;
                gym.GuestName = gname;
                gym.duration = duration;
                gym.payment = payment;
                gym.amount = amt;
                db.gymservices.Add(gym);
                db.SaveChanges();
            }
            if (duration == "2 Hour(Gymming)")
            {
                string amt = "Rs.1200 (Including Taxes)";
                gymservice gym = new gymservice();
                gym.PassportNumber = pno;
                gym.GuestName = gname;
                gym.duration = duration;
                gym.payment = payment;
                gym.amount = amt;
                db.gymservices.Add(gym);
                db.SaveChanges();
            }
            if (duration == "2 Hour(Gym + Sauna Bath)")
            {
                string amt = "Rs.1500 (Including Taxes)";
                gymservice gym = new gymservice();
                gym.PassportNumber = pno;
                gym.GuestName = gname;
                gym.duration = duration;
                gym.payment = payment;
                gym.amount = amt;
                db.gymservices.Add(gym);
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ActionResult ChildViewSpa()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChildViewSpa(string duration, string payment, string pno, string gname,string therapist)
        {
            passengersEntities1 db = new passengersEntities1();
            if (duration == "Shirodhara -- 1 hr")
            {
                string amt = "Rs.4500 (Including Taxes)";
                spaservice spa = new spaservice();
                spa.PassportNumber = pno;
                spa.GuestName = gname;
                spa.duration = duration;
                spa.amount = amt;
                spa.payment = payment;
                spa.therapist = therapist;
                db.spaservices.Add(spa);
                db.SaveChanges();

            }
            if (duration == "Head Massage -- 30 min")
            {
                string amt = "Rs.3000 (Including Taxes)";
                spaservice spa = new spaservice();
                spa.PassportNumber = pno;
                spa.GuestName = gname;
                spa.duration = duration;
                spa.amount = amt;
                spa.payment = payment;
                spa.therapist = therapist;
                db.spaservices.Add(spa);
                db.SaveChanges();
            }
            if (duration == "Foot Massage -- 1 hr")
            {
                string amt = "Rs.3500 (Including Taxes)";
                spaservice spa = new spaservice();
                spa.PassportNumber = pno;
                spa.GuestName = gname;
                spa.duration = duration;
                spa.amount = amt;
                spa.payment = payment;
                spa.therapist = therapist;
                db.spaservices.Add(spa);
                db.SaveChanges();
            }
            if (duration == "Abhyanga -- 1 hr")
            {
                string amt = "Rs.7500 (Including Taxes)";
                spaservice spa = new spaservice();
                spa.PassportNumber = pno;
                spa.GuestName = gname;
                spa.duration = duration;
                spa.amount = amt;
                spa.payment = payment;
                spa.therapist = therapist;
                db.spaservices.Add(spa);
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ActionResult Review()
        {
            var pno = Session["service"];
            var gymserice = db.GymServiceViews.Where(i => i.PassportNumber == pno.ToString()).ToList();
            return View(gymserice.ToList());
        }
    }
}

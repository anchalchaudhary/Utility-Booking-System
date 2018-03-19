﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtilityBookingSystem.Models;

namespace UtilityBookingSystem.Repository
{
    public class BookingRepository
    {
        #region Save new booking details to DB
        public int CreateNewBooking(Booking objBooking) //Saves booking details
        {
            int bookingID;
            using (BookingSystemDBEntities db = new BookingSystemDBEntities())
            {
                tblBooking objtblBooking = new tblBooking();
                objtblBooking.title = objBooking.title;
                objtblBooking.purposeID = objBooking.purposeID;
                objtblBooking.userID = objBooking.userID;
                objtblBooking.bookedAtTime = DateTime.Now;
                objtblBooking.status = 0;
                objtblBooking.isConfirmed = objBooking.isConfirmed;
                db.tblBookings.Add(objtblBooking);
                db.SaveChanges();

                bookingID = objtblBooking.bookingID; //fetches bookingID of new Booking

                tblBooking objtblBookingUpdate = db.tblBookings.SingleOrDefault(x => x.bookingID == bookingID);
                Random random = new Random();
                objtblBooking.bookingNo = "UBS" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + bookingID;

                db.SaveChanges();

            }
            return bookingID;
        }
        #endregion

        #region Save Booking Date
        //public int SaveBookingDate(DateTime date, int bookingID)
        public int SaveBookingDate(DetailsList item, int bookingID)
        {
            tblBookedDate objtblBookedDate = new tblBookedDate();

            using (BookingSystemDBEntities db = new BookingSystemDBEntities())
            {
                objtblBookedDate.bookingID = bookingID;
                objtblBookedDate.dateChosen = item.date.Date;
                objtblBookedDate.annexeRequired = item.annexeRequired;
                objtblBookedDate.photographer = item.photographer;
                objtblBookedDate.nonWorkingHours = item.nonWorkingHours;

                db.tblBookedDates.Add(objtblBookedDate);

                db.SaveChanges();
            }
            int dateID = objtblBookedDate.dateID;
            return dateID;
        }
        #endregion

        #region Save Selected Hall
        public void SaveSelectedHalls(List<Hall> hallsArray, int dateID, int bookingID)
        {
            int i;
            tblRequirementForHall objtblRequirementForHall = new tblRequirementForHall();
            using (var db = new BookingSystemDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                bool flagReq = false, flagSlot = false, flagHall = false;

                foreach (var item in hallsArray)
                {
                    flagHall = false;

                    if (hallsArray != null && item.hallID != 0)
                    {
                        int bookedHallID = 0;
                        if (item.requirementsArray != null)
                        {
                            foreach (var reqItem in item.requirementsArray)
                            {
                                tblBookedRequirement objtblBookedRequirement = new tblBookedRequirement();
                                if (reqItem.isSelected == true)
                                {
                                    flagReq = true;
                                    objtblBookedRequirement.dateID = dateID;
                                    objtblBookedRequirement.bookingID = bookingID;
                                    objtblRequirementForHall = db.tblRequirementForHalls.Where(x => x.requirementID == reqItem.requirementID && x.hallID == item.hallID).Single();
                                    objtblBookedRequirement.reqHallID = objtblRequirementForHall.reqHallID;

                                    db.tblBookedRequirements.Add(objtblBookedRequirement);
                                    db.SaveChanges();
                                }
                            }
                        }
                        if (item.slotsArray != null)
                        {
                            foreach (var slotItem in item.slotsArray)
                            {
                                tblBookedSlot objtblBookedSlot = new tblBookedSlot();
                                if (slotItem.isSelected == true)
                                {
                                    flagSlot = true;
                                    if (flagReq == true && flagSlot == true && flagHall == false)
                                    {
                                        tblBookedHall objtblBookedHall = new tblBookedHall();

                                        objtblBookedHall.hallID = item.hallID;
                                        objtblBookedHall.dateID = dateID;
                                        objtblBookedHall.otherRequirements = item.otherRequirements;

                                        db.tblBookedHalls.Add(objtblBookedHall);

                                        db.SaveChanges();

                                        bookedHallID = objtblBookedHall.bookedHallID;

                                        flagHall = true;
                                    }
                                    if (bookedHallID != 0)
                                    {
                                        objtblBookedSlot.bookedHallID = bookedHallID;
                                        objtblBookedSlot.slotID = slotItem.slotID;
                                        db.tblBookedSlots.Add(objtblBookedSlot);
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            using (var db = new BookingSystemDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                foreach (var item in hallsArray)
                {
                    tblChair objtblChair = new tblChair();
                    if (item.noOfChairs != 0)
                    {
                        objtblChair.noOfChairs = item.noOfChairs;
                        objtblChair.hallID = item.hallID;
                        objtblChair.bookingID = bookingID;
                        objtblChair.dateID = dateID;

                        db.tblChairs.Add(objtblChair);
                        db.SaveChanges();
                    }
                }
            }
        }
            #endregion

            #region Save selected Requirements
            public void SaveSelectedRequirements(int[] hallsArray, int[] requirementsArray, int bookingID, int dateID)
            {
                int i;
                tblBookedRequirement objtblBookedRequirement = new tblBookedRequirement();

                using (BookingSystemDBEntities db = new BookingSystemDBEntities())
                {
                    for (i = 0; i < requirementsArray.Length; i++)
                    {
                        objtblBookedRequirement.dateID = dateID;
                        objtblBookedRequirement.bookingID = bookingID;
                        objtblBookedRequirement.reqHallID = requirementsArray[i];

                        db.tblBookedRequirements.Add(objtblBookedRequirement);
                        db.SaveChanges();
                    }
                }
            }
            #endregion
        }
    }

using System;
using System.Globalization;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels.EmployeeResponseModel;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Models.ModelMapers.NotificationMapper
{
    public static class NotificationMapper
    {
        public static NotificationResponse CreateFromServerToClient(this Notification notification)
        {
            return new NotificationResponse
            {
                NotificationId = notification.NotificationId,
                TitleA = notification.TitleA,
                TitleE = notification.TitleE,
                CategoryId = notification.CategoryId,
                AlertBefore = notification.AlertBefore,
                AlertDateType = notification.AlertDateType,
                AlertDate = notification.AlertDate.ToString("dd/MM/yyyy", new CultureInfo("en")),
                EmployeeId = notification.EmployeeId,
                MobileNo = notification.MobileNo,
                Email = notification.Email,
                ReadStatus = notification.ReadStatus,

                RecCreatedBy = notification.RecCreatedBy,
                RecCreatedDate = notification.RecCreatedDate,
                RecLastUpdatedBy = notification.RecLastUpdatedBy,
                RecLastUpdatedDate = notification.RecLastUpdatedDate
            };
        }
        public static Notification CreateFromClientToServer(this NotificationResponse notification)
        {
            DateTime alertAppearDate = new DateTime(); 
            switch (notification.AlertBefore)
            {
                case 1: alertAppearDate = DateTime.ParseExact(notification.AlertDate, "dd/MM/yyyy", new CultureInfo("en")).AddMonths(-1); break;
                case 2: alertAppearDate = DateTime.ParseExact(notification.AlertDate, "dd/MM/yyyy", new CultureInfo("en")).AddDays(-7); break;
                case 3: alertAppearDate = DateTime.ParseExact(notification.AlertDate, "dd/MM/yyyy", new CultureInfo("en")).AddDays(-1); break;
            }
            return new Notification
            {
                NotificationId = notification.NotificationId,
                TitleA = notification.TitleA,
                TitleE = notification.TitleE,
                CategoryId = notification.CategoryId,
                AlertBefore = notification.AlertBefore,
                AlertDateType = notification.AlertDateType,
                AlertDate = DateTime.ParseExact(notification.AlertDate, "dd/MM/yyyy", new CultureInfo("en")),
                AlertAppearDate = alertAppearDate,
                EmployeeId = notification.EmployeeId,
                MobileNo = notification.MobileNo,
                Email = notification.Email,
                ReadStatus = notification.ReadStatus,
                SystemGenerated = notification.SystemGenerated,

                RecCreatedBy = notification.RecCreatedBy,
                RecCreatedDate = notification.RecCreatedDate,
                RecLastUpdatedBy = notification.RecLastUpdatedBy,
                RecLastUpdatedDate = notification.RecLastUpdatedDate
            };
        }
        public static EmployeeDDL CreateForEmployeeDDL(this Employee source)
        {
            return new EmployeeDDL
            {
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.EmployeeNameE,
                EmployeeNameA = source.EmployeeNameA,
                Email = source.Email,
                MobileNo = source.EmployeeMobileNum
            };
        }
        public static NotificationListResponse CreateFromServerToClientList(this Notification notification)
        {
            NotificationListResponse notificationListResponse=new NotificationListResponse();
            notificationListResponse.NotificationId = notification.NotificationId;
            notificationListResponse.NotificationName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en" ? notification.TitleE : notification.TitleA;      
            notificationListResponse.AlertEndTime = notification.AlertDate.ToString("dd/MM/yyyy", new CultureInfo("en"));
            if (notification.EmployeeId != null)
            {
                notificationListResponse.EmployeeId = Convert.ToInt64(notification.EmployeeId);
                notificationListResponse.EmployeeName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en" ? notification.Employee.EmployeeNameE : notification.Employee.EmployeeNameA;
            }
            
            notificationListResponse.MobileNo = notification.MobileNo;
            notificationListResponse.Email = notification.Email;
            notificationListResponse.Notified = notification.ReadStatus ? Resources.Notification.Yes : Resources.Notification.No;
           
            switch (notification.CategoryId)
            {
                case 1: notificationListResponse.CategoryName = Resources.Notification.Company; break;
                case 2: notificationListResponse.CategoryName = Resources.Notification.Documents; break;
                case 3: notificationListResponse.CategoryName = Resources.Notification.Employees; break;
                case 4: notificationListResponse.CategoryName = Resources.Notification.Meetings; break;
                case 5: notificationListResponse.CategoryName = Resources.Notification.Other; break;
                default: notificationListResponse.CategoryName = Resources.Notification.Other; break;
            }
            switch (notification.AlertBefore)
            {
                case 1: notificationListResponse.AlertTime = Resources.Notification.BeforeOneMonth; break;
                case 2: notificationListResponse.AlertTime = Resources.Notification.BeforeOneWeek; break;
                case 3: notificationListResponse.AlertTime = Resources.Notification.BeforeOneDay; break;
            }

            return notificationListResponse;
        }
    }
}

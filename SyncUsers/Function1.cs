using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace SyncUsers
{
    public class SyncUsers
    {
        //private readonly ILogger _logger;

        //public SyncUsers(ILoggerFactory loggerFactory)
        //{
        //    _logger = loggerFactory.CreateLogger<SyncUsers>();
        //}

        [Function("SyncUsersBetweenDepartments")]
        public void Run([TimerTrigger("0 0 * * * *")] TimerInfo myTimer, ILogger logger)
        {
            // Fetch list of users from each context corresponding to the 2 departments
            DAL.Context contextWiley = new DAL.Context();
            DAL.SyncContext contextWidget = new DAL.SyncContext();

            List<Domain.Customer> wileyAndCoUsers = contextWiley.Customers.ToList();
            List<Domain.Customer> widgetAndCoUsers = contextWidget.Customers.ToList();

            // Compare each user and synchronize their data
            foreach(Domain.Customer wc in widgetAndCoUsers) 
            {
                Domain.Customer correspondingWileyAndCoUser = wileyAndCoUsers.FirstOrDefault(u => u.CustomerID == wc.CustomerID);

                // If the user does not exist in the Wiley and Co context, add them
                if (correspondingWileyAndCoUser ==  null)
                {
                    contextWiley.Customers.Add(wc);
                }

                // Update Wiley and Co user
                else
                {
                    correspondingWileyAndCoUser = wc;
                }

                contextWiley.SaveChanges();
            }


            logger.LogInformation($"Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }

        }

    }
}

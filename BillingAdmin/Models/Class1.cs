using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BillingAdmin.Models;
using static BillingAdmin.Models.Utils;

namespace BillingAdmin.Models
{
    public class IndexRolesRepository : IPaginationIndexRoles<IndexRole>
    {
        Entities db = new Entities();
        public IQueryable<IndexRole> GetPaginated(ControllerContext cont, string filter, int initialPage, int pageSize, out int totalRecords, out int recordsFiltered)
        {
            Entities Context = new Entities();
            IQueryable<AspNetUsers> data = from a in db.AspNetUsers
                                           select a;

            totalRecords = data.Count();

            List<IndexRole> res = new List<IndexRole>();

            recordsFiltered = data.Count();

            data = data.OrderByDescending(x => x.Id)
           .Skip((initialPage * pageSize))
           .Take(pageSize);
            Task userRolesLoad = Task.Run(delegate
            {
                foreach (var aspNetUser in data)
                {
                    IndexRole indexRole = new IndexRole
                    {
                        Id = aspNetUser.Id,
                        Email = aspNetUser.Email,
                        UserName = aspNetUser.UserName
                    };

                    indexRole.UserRoles = GetUserRoles(indexRole.Id);
                    res.Add(indexRole);
                }
            });

            IEnumerable<IndexRole> result = null;

            try
            {
                if (cont.HttpContext.Request.Form.AllKeys.Contains("order[0][column]"))
                {
                    int i = int.Parse(cont.HttpContext.Request.Form["order[0][column]"]);

                    switch (cont.HttpContext.Request.Form["order[0][dir]"])
                    {
                        case "desc":
                            switch (i)
                            {
                                case 1:
                                    result = res.OrderByDescending(x => x.Id)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 2:
                                    result = res.OrderByDescending(x => x.Email)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 3:
                                    result = res.OrderByDescending(x => x.UserName)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 4:
                                    result = res.OrderByDescending(x => x.UserRoles)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;

                                default:
                                    break;
                            }
                            break;

                        case "asc":
                            switch (i)
                            {
                                case 1:
                                    result = res.OrderBy(x => x.Id)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 2:
                                    result = res.OrderBy(x => x.Email)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 3:
                                    result = res.OrderBy(x => x.UserName)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 4:
                                    result = res.OrderBy(x => x.UserRoles)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                default:
                                    break;
                            }
                            break;
                    }
                    //  }
                    //}

                }
            }
            catch (Exception)
            {

                throw;
            }

            if (result == null)
            {
                result = res.AsQueryable().OrderBy(x => x.Id);
                //.Skip((initialPage * pageSize))
                //.Take(pageSize);
            }

            userRolesLoad.Wait();
            //data = data.OrderBy(x => x.FullLegalName)
            //            .Skip((initialPage * pageSize))
            //            .Take(pageSize);
            return result.AsQueryable();
        }

        public string GetUserRoles(string UserId)
        {
            string userRoles = "";
            foreach (var userRole in db.AspNetUsers.Find(UserId).AspNetRoles.AsParallel().Select(x => x.Name).ToList())
            {
                userRoles += userRole + " ";
            }
            return userRoles;
        }
    }
    public interface IPaginationIndexRoles<T> where T : class
    {
        /// <summary>
        /// Get data paginated
        /// </summary>
        /// <param name="filter">Filter applied by user</param>
        /// <param name="initialPage">Initial page</param>
        /// <param name="pageSize">Amount of records for each page</param>
        /// <param name="totalRecords">Total of records</param>
        /// <param name="recordsFiltered">Total records filtered when filter applied</param>
        /// <returns>List of all categories found</returns>      

        IQueryable<T> GetPaginated(ControllerContext cont, string filter, int initialPage, int pageSize, out int totalRecords, out int recordsFiltered);
    }

    public class ClientRepository : IPaginationClient<Client>
    {
        Entities Context = new Entities();
        public IQueryable<Client> GetPaginated(ControllerContext cont, string filter, int initialPage, int pageSize, out int totalRecords, out int recordsFiltered)
        {
            IQueryable<Accounts> data = from a in Context.Accounts
                                        select a;

            totalRecords = data.Count();


            if (!string.IsNullOrEmpty(filter))
            {
                List<long> accs = new List<long>();

                if (filter.Contains("@"))
                {
                    try
                    {
                        var users = Context.AspNetUsers.Where(c => c.Email.Contains(filter)).Select(c => c.Id).ToList();

                        accs = Context.Accounts_Users.Where(c => users.Contains(c.userId)).Select(c => c.accountId).ToList();
                    }
                    catch (Exception)
                    {

                        // throw;
                    }
                }
                data = data.Where(x => accs.Contains(x.Id) || x.FullLegalName.ToUpper().Contains(filter.ToUpper()) || x.DirectorFullName.ToUpper().Contains(filter.ToUpper()) || x.ShortLegalName.ToUpper().Contains(filter.ToUpper()) || x.Id.ToString().Contains(filter) || x.INN.ToString().Contains(filter) || x.Email.ToString().Contains(filter) || x.Phone.ToString().Contains(filter) || x.Legal_Street.ToString().Contains(filter));
            }

            recordsFiltered = data.Count();
            var res = from c in data
                      select new Client
                      {
                          Id = c.Id,
                          ContactName = c.ContactName,
                          DirectorFullName = c.DirectorFullName,
                          Email = c.Email,
                          INN = c.INN,
                          ShortLegalName = c.ShortLegalName
                      };

            IEnumerable<Client> result = null;

            try
            {
                if (cont.HttpContext.Request.Form.AllKeys.Contains("order[0][column]"))
                {
                    int i = int.Parse(cont.HttpContext.Request.Form["order[0][column]"]);

                    switch (cont.HttpContext.Request.Form["order[0][dir]"])
                    {
                        case "desc":
                            switch (i)
                            {
                                case 1:
                                    result = res.OrderByDescending(x => x.Id)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 2:
                                    result = res.OrderByDescending(x => x.Email)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 3:
                                    result = res.OrderByDescending(x => x.DirectorFullName)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 4:
                                    result = res.OrderByDescending(x => x.ContactName)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 5:
                                    result = res.OrderByDescending(x => x.INN)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 6:
                                    result = res.OrderByDescending(x => x.ShortLegalName)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case "asc":
                            switch (i)
                            {
                                case 1:
                                    result = res.OrderBy(x => x.Id)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 2:
                                    result = res.OrderBy(x => x.Email)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 3:
                                    result = res.OrderBy(x => x.ContactName)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 4:
                                    result = res.OrderBy(x => x.DirectorFullName)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 5:
                                    result = res.OrderBy(x => x.INN)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 6:
                                    result = res.OrderBy(x => x.ShortLegalName)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                default:
                                    break;
                            }
                            break;
                    }
                    //  }
                    //}

                }
            }
            catch (Exception)
            {

                throw;
            }

            if (result == null)
            {
                result = res.AsQueryable().OrderBy(x => x.Id)
                            .Skip((initialPage * pageSize))
                            .Take(pageSize); ;
            }

            //data = data.OrderBy(x => x.FullLegalName)
            //            .Skip((initialPage * pageSize))
            //            .Take(pageSize);
            return result.AsQueryable();
        }
    }

    public interface IPaginationClient<T> where T : class
    {
        /// <summary>
        /// Get data paginated
        /// </summary>
        /// <param name="filter">Filter applied by user</param>
        /// <param name="initialPage">Initial page</param>
        /// <param name="pageSize">Amount of records for each page</param>
        /// <param name="totalRecords">Total of records</param>
        /// <param name="recordsFiltered">Total records filtered when filter applied</param>
        /// <returns>List of all categories found</returns>      

        IQueryable<T> GetPaginated(ControllerContext cont, string filter, int initialPage, int pageSize, out int totalRecords, out int recordsFiltered);
    }
    public class Statistic_pbxRepository : IPaginationClient<Statistic_pbxModel>
    {
        Entities db = new Entities();

        public IQueryable<Statistic_pbxModel> GetPaginated(ControllerContext cont, string filter, int initialPage, int pageSize, out int totalRecords, out int recordsFiltered)
        {
            List<Statistic_pbx> data = db.Statistic_pbx.OrderByDescending(c => c.id).Where(c => c.billsec > 0).AsNoTracking().ToList();

            totalRecords = data.Count();

            List<int> counts = new List<int>();
            recordsFiltered = data.Count();
            var res = from c in data
                      select new Statistic_pbxModel
                      {
                          id = c.id,
                          src_num = c.src_num,
                          dst_num = c.dst_num,
                          recordingfile = c.recordingfile,
                          Recording = c.UNIQUEID.Contains(".") ? c.UNIQUEID.Split('.')[1] : c.UNIQUEID,
                          UNIQUEID = c.UNIQUEID,
                          dialstatus = c.dialstatus,
                          duration = c.duration,
                          billsec = c.billsec,
                      };

            IEnumerable<Statistic_pbxModel> result = null;
            try
            {
                if (cont.HttpContext.Request.Form.AllKeys.Contains("order[0][column]"))
                {
                    int i = int.Parse(cont.HttpContext.Request.Form["order[0][column]"]);

                    switch (cont.HttpContext.Request.Form["order[0][dir]"])
                    {
                        case "desc":
                            switch (i)
                            {
                                case 1:
                                    result = res.OrderByDescending(x => x.id)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 2:
                                    result = res.OrderByDescending(x => x.src_num)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 3:
                                    result = res.OrderByDescending(x => x.dst_num)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 4:
                                    result = res.OrderByDescending(x => x.recordingfile)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 5:
                                    result = res.OrderByDescending(x => x.UNIQUEID)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 6:
                                    result = res.OrderByDescending(x => x.duration)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 7:
                                    result = res.OrderByDescending(x => x.billsec)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case "asc":
                            switch (i)
                            {
                                case 1:
                                    result = res.OrderBy(x => x.id)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 2:
                                    result = res.OrderBy(x => x.src_num)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 3:
                                    result = res.OrderBy(x => x.dst_num)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 4:
                                    result = res.OrderBy(x => x.recordingfile)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 5:
                                    result = res.OrderBy(x => x.UNIQUEID)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 6:
                                    result = res.OrderBy(x => x.duration)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                case 7:
                                    result = res.OrderBy(x => x.billsec)
                                                .Skip((initialPage * pageSize))
                                                .Take(pageSize);
                                    break;
                                default:
                                    break;
                            }
                            break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            if (result == null)
            {
                result = res.AsQueryable().OrderByDescending(c => c.id)
                            .Skip((initialPage * pageSize));
            }

            return result.AsQueryable();
        }
    }

    public class Class1
    {
    }
}
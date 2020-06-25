using Qualification.Dto;
using Qualification.Models;
using System.Linq;

namespace Qualification.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> FilterEmployees<T>(
            this IQueryable<T> query,
            UserFilterDto filterParams) 
            where T : UserDto
        {
            if(filterParams == null)
            {
                return query;
            }

            if (!string.IsNullOrEmpty(filterParams.Email))
            {
                query = query.Where(x => x.Email.ToLower().Contains(filterParams.Email.ToLower()));
            }

            if (!string.IsNullOrEmpty(filterParams.PhoneNumber))
            {
                query = query.Where(x => x.PhoneNumber.ToLower().Contains(filterParams.PhoneNumber.ToLower()));
            }

            if (!string.IsNullOrEmpty(filterParams.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(filterParams.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(filterParams.SurName))
            {
                query = query.Where(x => x.Surname.ToLower().Contains(filterParams.SurName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filterParams.MiddleName))
            {
                query = query.Where(x => x.MiddleName.ToLower().Contains(filterParams.MiddleName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filterParams.Education))
            {
                query = query.Where(x => x.Education.ToLower().Contains(filterParams.Education.ToLower()));
            }

            if (!string.IsNullOrEmpty(filterParams.HistoryOfWork))
            {
                query = query.Where(x => x.HistoryOfWork.ToLower().Contains(filterParams.HistoryOfWork.ToLower()));
            }

            return query;
        }

        public static IQueryable<T> FilterEmployers<T>(
            this IQueryable<T> query,
            UserFilterDto filterParams)
            where T : UserDto
        {
            if (filterParams == null)
            {
                return query;
            }

            if (!string.IsNullOrEmpty(filterParams.Email))
            {
                query = query.Where(x => x.Email.ToLower().Contains(filterParams.Email.ToLower()));
            }

            if (!string.IsNullOrEmpty(filterParams.PhoneNumber))
            {
                query = query.Where(x => x.PhoneNumber.ToLower().Contains(filterParams.PhoneNumber.ToLower()));
            }

            if (!string.IsNullOrEmpty(filterParams.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(filterParams.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(filterParams.SurName))
            {
                query = query.Where(x => x.Surname.ToLower().Contains(filterParams.SurName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filterParams.MiddleName))
            {
                query = query.Where(x => x.MiddleName.ToLower().Contains(filterParams.MiddleName.ToLower()));
            }

            return query;
        }
    }
}

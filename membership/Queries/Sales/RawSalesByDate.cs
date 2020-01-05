using Membership.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Membership.Queries.Sales
{
    public class RawSale
    {
        public string Title { get; set; }
        public decimal Total { get; set; }
        public double Quarter { get; set; }
        public double Year { get; set; }
        public double Month { get; set; }
        public string QuarterYear { get; set; }
    }
    public class RawSalesByDate
    {
        ICommandRunner runner;
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Quarter { get; set; }
        public RawSalesByDate(ICommandRunner runner)
        {
            this.runner = runner;
        }

        public IEnumerable<RawSale> Execute()
        {
            var args = new List<object>();
            var sqlFormat = @"select distinct title, quarter, year, qyear as QaurterYear," +
                @"sum(amount) over (partition by title {0}) as Total from raw_sales where 1 = 1 {1}";
            string partitionValue = "";
            string whereValue = "";
            if (this.Year.HasValue)
            {
                args.Add(this.Year.Value);
                partitionValue = ",year";
                whereValue = "and year = 2007";
            }
            if (this.Quarter.HasValue)
            {
                if (!this.Year.HasValue)
                {
                    this.Year = DateTime.Today.Year;
                }
                args.Add(this.Quarter.Value);
                partitionValue = ",year, quarter";
                whereValue = "and year = @0 and quarter = @1";
            }
            if (this.Month.HasValue){
                if (!this.Year.HasValue)
                {
                    this.Year = DateTime.Today.Year;
                }
                args.Add(this.Month.Value);
                partitionValue = ",year, month";
                whereValue = "and year = @0 and month = @1";
            }

            var sql = string.Format(sqlFormat, partitionValue, whereValue);

            return this.runner.Execute<RawSale>(sql, args.ToArray());
        }
    }
}

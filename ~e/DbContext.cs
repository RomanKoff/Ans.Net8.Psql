using Microsoft.EntityFrameworkCore;

namespace Ans.Net8.Psql
{

	public static partial class _e
	{

		/*
         * void SqlClearTable(this DbContext context, string table);
		 * int ExecuteSqlRawIfPresent(this DbContext context, string sql);
		 * int SerialSequenceSetMax(this DbContext context, string table);
		 * int CreateFunction_DateUpdate(this DbContext context);
		 * int CreateTrigger_DateUpdate(this DbContext context, string table);
         */


		public static void SqlClearTable(
			this DbContext context,
			string table)
		{
			_ = context.Database.ExecuteSqlRaw(
				$"delete from \"{table}\";" +
				$"select setval(pg_get_serial_sequence('public.\"{table}\"', 'Id'), 1);");
			_ = context.SaveChanges();
		}


		public static int ExecuteSqlRawIfPresent(
			this DbContext context,
			string sql)
		{
			if (string.IsNullOrEmpty(sql))
				return 0;
			return context.Database.ExecuteSqlRaw(sql);
		}


		public static int SerialSequenceSetMax(
			this DbContext context,
			string table)
		{
			return context.Database.ExecuteSql(
@$"select setval(
	pg_get_serial_sequence('public.""{table}""', 'Id'),
	(select max(""Id"") from public.""{table}"")
);");
		}


		public static int CreateFunction_DateUpdate(
			this DbContext context)
		{
			return context.Database.ExecuteSqlRaw(@"
CREATE OR REPLACE FUNCTION func_DateUpdate()
RETURNS TRIGGER AS $$
BEGIN
   NEW.""DateUpdate"" = LOCALTIMESTAMP; 
   RETURN NEW;
END;
$$ language 'plpgsql';");
		}


		public static int CreateTrigger_DateUpdate(
			this DbContext context,
			string table)
		{
			return context.Database.ExecuteSql(@$"
CREATE OR REPLACE TRIGGER trigger_{table}_DateUpdate
BEFORE UPDATE ON public.""{table}""
FOR EACH ROW EXECUTE PROCEDURE func_DateUpdate();
");
		}

	}

}

﻿using Ans.Net8.Common;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Ans.Net8.Psql
{

	public static partial class _e
	{

		/* methods */


		public static void SqlReassignOwned(
			this DbContext context,
			string owner)
		{
			_ = context.Database.ExecuteSqlRaw(
				$"reassign owned by CURRENT_ROLE" +
				$" to \"{owner}\";");
			_ = context.SaveChanges();
			Debug.WriteLine($"SqlReassignOwned(\"{owner}\")");
		}


		public static void SqlAlterTableOwnerTo(
			this DbContext context,
			string table,
			string owner)
		{
			_ = context.Database.ExecuteSqlRaw(
				$"alter table \"{table}\"" +
				$" owner to \"{owner}\";");
			_ = context.SaveChanges();
			Debug.WriteLine($"SqlAlterTableOwnerTo(\"{table}\", \"{owner}\")");
		}


		/* functions */


		public static int SqlClearTable(
			this DbContext context,
			string table)
		{
			_ = context.Database.ExecuteSqlRaw(
				$"delete from \"{table}\";" +
				$"select setval(pg_get_serial_sequence('public.\"{table}\"', 'Id'), 1, 'f');");
			var count1 = context.SaveChanges();
			Debug.WriteLine($"SqlClearTable(\"{table}\")");
			return count1;
		}


		public static (string, DbContext) GetTableNameAndContext<T>(
			this DbSet<T> dbSet)
			where T : class
		{
			var context1 = dbSet.GetDbContext();
			var type1 = context1.Model.FindEntityType(typeof(T));
			return (type1.GetTableName(), context1);
		}


		public static int SqlClearTable<T>(
			this DbSet<T> dbSet)
			where T : class
		{
			var (name1, context1) = dbSet.GetTableNameAndContext();
			return context1.SqlClearTable(name1);
		}


		public static int SerialSequenceSetMax<T>(
			this DbSet<T> dbSet)
			where T : class
		{
			var (name1, context1) = dbSet.GetTableNameAndContext();
			return context1.SerialSequenceSetMax(name1);
		}


		public static int Migration<T>(
			this DbSet<T> dbSet,
			IEnumerable<object> items,
			string fieldsDefinitions)
			where T : class
		{
			var (name1, context1) = dbSet.GetTableNameAndContext();
			var helper1 = new MigrationTableHelper(name1, fieldsDefinitions);
			foreach (var item1 in items)
				helper1.AddInsert(item1);
			var count1 = context1.ExecuteSqlRawIfPresent(helper1.Sql.ToString());
			_ = context1.SerialSequenceSetMax(name1);
			return count1;
		}


		public static int ExecuteSqlRawIfPresent(
			this DbContext context,
			string sql)
		{
			return (string.IsNullOrEmpty(sql))
				? 0 : context.Database.ExecuteSqlRaw(sql);
		}


		public static int SerialSequenceSetMax(
			this DbContext context,
			string table)
		{
			return context.Database.ExecuteSqlRaw(
				SuppSql.GetPsqlSS(table, "Id"));
		}


		public static int CreateFunction_DateUpdate(
			this DbContext context)
		{
			var sql1 = @"
CREATE OR REPLACE FUNCTION func_dateupdate()
RETURNS TRIGGER AS $$
BEGIN
   NEW.""DateUpdate"" = LOCALTIMESTAMP; 
   RETURN NEW;
END;
$$ language 'plpgsql';";
			return context.Database.ExecuteSqlRaw(sql1);
		}


		public static int CreateTrigger_DateUpdate(
			this DbContext context,
			string table)
		{
			var sql1 = @$"
CREATE OR REPLACE TRIGGER trigger_{table}_dateupdate
BEFORE UPDATE ON public.""{table}""
FOR EACH ROW EXECUTE PROCEDURE func_dateupdate();
";
			return context.Database.ExecuteSqlRaw(sql1);
		}

	}

}

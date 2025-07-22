using Ans.Net8.Common;
using Microsoft.EntityFrameworkCore;

namespace Ans.Net8.Psql
{

	public static class SuppMigration
	{

		/* methods */


		public static void MigrationTable<TSource, TTarget>(
			IQueryable<TSource> source,
			DbSet<TTarget> target,
			Func<TSource, TTarget> newItem,
			int bufferCount = 500)
			where TSource : class
			where TTarget : class
		{
			var (name1, db1) = target.GetTableNameAndContext();
			Console.Write($"{name1}: ");
			if (target.Any())
			{
				Console.WriteLine(SuppLangEn.GetDeclineEn(
					"contains {0} {1}.",
					target.Count(),
					"entity",
					"entites"));
			}
			else
			{
				SuppConsole.CursorSavePos();
				var c1 = source.Count();
				var i1 = 0;
				foreach (var item1 in source)
				{
					target.Add(newItem(item1));
					if (bufferCount != 0 && i1 > bufferCount)
					{
						db1.SaveChanges();
						i1 = 0;
					}
					i1++;
					Console.Write($"{c1--}   ");
					SuppConsole.CursorRestopePos();
				}
				db1.SaveChanges();
				db1.SerialSequenceSetMax($"{name1}");
				Console.WriteLine(SuppLangEn.GetDeclineEn(
					"{0} {1} added.",
					target.Count(),
					"entity",
					"entites"));
			}
			Console.WriteLine();
		}

	}

}

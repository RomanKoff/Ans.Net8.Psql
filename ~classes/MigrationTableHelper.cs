using Ans.Net8.Common;
using System.Text;

namespace Ans.Net8.Psql
{

	/// <param name="fieldsDefinitions">
	/// "fileld1_definition|fileld2_definition..."
	/// </param>
	public class MigrationTableHelper(
		string name,
		string fieldsDefinitions)
	{


		/* readonly properties */


		public StringBuilder Sql { get; } = new StringBuilder();

		public IEnumerable<MigrationField> Fields { get; } = fieldsDefinitions.Split('|')
			.Select(x => new MigrationField(x));

		public string Name { get; } = name;


		/* methods */


		public void AddInsert(
			object item)
		{
			var fields1 = new List<string>();
			var values1 = new List<string>();
			foreach (var field1 in Fields)
			{
				var value1 = item.GetPropertyValue(field1.Name1);
				if (value1 != null)
				{
					string s1 = field1.FuncToString(value1);
					if (!string.IsNullOrEmpty(s1) && s1 != "NULL")
					{
						fields1.Add(field1.Name2);
						values1.Add(s1);
					}
				}
			}
			if (fields1.Count > 0)
			{
				var fields2 = fields1.MakeFromCollection(null, "\"{0}\"", ",");
				var values2 = values1.MakeFromCollection(null, null, ",");
				Sql.AppendLine(
					$"insert into \"{Name}\" ({fields2}) values ({values2});");
				//Sql.AppendLine();
			}
		}


		public void AddResetSequence(
			string key)
		{
			Sql.AppendLine();
			Sql.AppendLine(
				$"select setval(" +
				$"pg_get_serial_sequence('public.\"{Name}\"', '{key}'), " +
				$"(select max(\"{key}\") from public.\"{Name}\")" +
				$");");
			Sql.AppendLine();
		}


		public void AddInserts<TEntity>(
			IEnumerable<TEntity> items,
			string key)
		{
			foreach (var item1 in items)
				AddInsert(item1);
			AddResetSequence(key);
		}

	}

}

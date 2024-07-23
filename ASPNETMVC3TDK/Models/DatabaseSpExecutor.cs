namespace ASPNETMVC3TDK.Models
{
	using PetaPoco;
	using System.Collections.Generic;
	using System.IO;

	public class DatabaseSpExecutor
	{
		private readonly Database _db;

		public DatabaseSpExecutor(Database database)
		{
			_db = database;
		}

		public List<T> Fetch<T>(string sqlQuery)
		{


			// Execute the query against the database and return the result
			return _db.Fetch<T>(";" + sqlQuery);
		}

		public void Close()
		{

		}
	}

}
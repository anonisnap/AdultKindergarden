using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Models;

namespace FileData
{
	public class FileContext
	{
		public IList<Family> Families { get; private set; }
		//public IList<Adult> Adults { get; private set; }

		private const string FamiliesFile = "families.json";
		//private const string AdultsFile = "adults.json";

		public FileContext()
		{
			Families = File.Exists(FamiliesFile) ? ReadData<Family>(FamiliesFile) : new List<Family>();
			//Adults = File.Exists(AdultsFile) ? ReadData<Adult>(AdultsFile) : new List<Adult>();
		}

		private IList<T> ReadData<T>(string s)
		{
			using (var jsonReader = File.OpenText(FamiliesFile))
			{
				return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd());
			}
		}

		public void SaveChanges()
		{
			// storing families
			string jsonFamilies = JsonSerializer.Serialize(Families, new JsonSerializerOptions {WriteIndented = true});
			using (StreamWriter outputFile = new StreamWriter(FamiliesFile, false))
			{
				outputFile.Write(jsonFamilies);
			}

			// storing persons
			//string jsonAdults = JsonSerializer.Serialize(Adults, new JsonSerializerOptions { WriteIndented = true });
			//using (StreamWriter outputFile = new StreamWriter(AdultsFile, false)) {
			//	outputFile.Write(jsonAdults);
			//}
		}
	}
}
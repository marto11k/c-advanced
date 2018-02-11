using System;
using System.Collections.Generic;
using System.Linq;

namespace hospital
{
	class Program
	{
		static void Main(string[] args)
		{
			var departments = new Dictionary<string, List<string>>();
			var doctors = new Dictionary<string, List<string>>();

			var input = string.Empty;
			while ((input = Console.ReadLine()) != "Output")
			{
				var patientData = input.Split();
				var department = patientData[0];
				var doctor = patientData[1] + " " + patientData[2];
				var patient = patientData[3];

				if (!departments.ContainsKey(department))
					departments[department] = new List<string>();
				departments[department].Add(patient);

				if (!doctors.ContainsKey(doctor))
					doctors[doctor] = new List<string>();
				doctors[doctor].Add(patient);

			}
			input = Console.ReadLine();
			do
			{
				var tokens = input.Split();
				if (tokens.Length == 1)
				{
					foreach (var patient in departments[tokens[0]])
					{
						Console.WriteLine(patient);
					}
				}
				else
				{
					var room = 0;
					if (int.TryParse(tokens[1], out room))
					{
						var skip = 3 * (room - 1);
						foreach (var patient in departments[tokens[0]].Skip(skip).Take(3).OrderBy(n => n))
						{
							Console.WriteLine(patient);
						}
					}
					else
					{
						var doctor = tokens[0] + " " + tokens[1];
						foreach (var patient in doctors[doctor].OrderBy(n => n))
						{
							Console.WriteLine(patient);
						}
					}
				}

			} while ((input = Console.ReadLine()) != "End");
		}
	}
}
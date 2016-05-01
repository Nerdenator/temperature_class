using System;

namespace temperature_class
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Boolean numOnly = false; // value to see if ID is numeric only. Gets set to true if validation is positive
			Boolean correctTemp = false; // value to see if temp is actually a temp. Gets set to true if validation is positive. 
			// print main header, then ask for person id to store the information under
			LineOutputs.MainHeader ();
			LineOutputs.EnterPersonId ();
			// validate the person id as only containing numeric chars. will continue until it gets numeric only.
			var unvalidatedId = Console.ReadLine();
			numOnly = Validators.validateId (unvalidatedId);
			while (numOnly == false) {
				LineOutputs.InvalidId();
				unvalidatedId = Console.ReadLine ();
				numOnly = Validators.validateId (unvalidatedId);
			}
			var validatedId = Convert.ToDecimal(unvalidatedId);
			// if successful, escape while, create new tempsubject to put the information in
			var subject = new TempSubject(validatedId);


			// ask for temperature
			LineOutputs.EnterTemp();
			var unvalidatedTemp = Console.ReadLine ();
			correctTemp = Validators.validateNumeric (unvalidatedTemp);
			while (correctTemp == false) {
				LineOutputs.InvalidTemp ();
				unvalidatedTemp = Console.ReadLine ();
				correctTemp = Validators.validateNumeric ();
			}
			var validatedTemp = Convert.ToDecimal (unvalidatedTemp);
			// dump into object
			subject.PatientTemp = validatedTemp;

			// ask for temp unit
		}
	}

	class Validators
	{
		public static Boolean validateId(string idText){
			var cCount = 0;
			foreach (char c in idText) {
				if (Char.IsLetter (c) || Char.IsPunctuation(c) || Char.IsWhiteSpace(c) || Char.IsControl(c))
					cCount++;
			}
			if (cCount > 0) {
				return false;
			} else {
				return true;
			}
		}

		public static Boolean validateNumeric(string text){
			var cCount = 0;
			foreach (char c in text) {
				if (Char.IsDigit || Char.IsNumber || c == ".")	// add validation to make sure there's only one decimal
					cCount++;
			}
			if (cCount == text.Length)
				return true;
			else
				return false;
		}
	}

	class Separator
	{
		protected static void Dashes()
		{
			for (int i = 1; i < 81; i++){
				Console.Write("-");
			}
			Console.Write("\n");
			return;
		}
	}



	class LineOutputs : Separator
	{
		public static void MainHeader(){
			Console.WriteLine ("Welcome to the temperature program.");
			Dashes ();
			return;
		}
		public static void InvalidTemp(){
			Console.WriteLine ("I'm sorry, that isn't a valid temperature.");
			Separator.Dashes ();
			return;
		}
		public static void InvalidId(){
			Console.WriteLine ("I'm sorry, that isn't a valid ID (only numbers, no symbols or letters). Please try again: ");
			return;
		}

		public static void EnterTemp(){
			Console.WriteLine ("Please enter a temperature, up to hundredth of a degree: ");
			Separator.Dashes ();
			return;
		}
		public static void EnterPersonId(){
			Console.WriteLine ("Please enter an ID for the person who's having his or her temperature taken: ");
			return;
		}
		public static void EnterUnits(){
			Console.WriteLine ("Please select a unit of temperature; (C)elsius or (F)ahrenheit: ");
			return;
		}
		public static void EnterSite(){
			Console.WriteLine ("Please enter the site of the measurement on the subject's body: ");
		}
	}

	class TempSubject
	{
		// fields
		private Decimal patientId; 
		private Decimal patientTemp;
		private String temperatureUnit;
		private String temperatureLocation;

		// properties
		public Decimal PatientId {
			get { return patientId; }
			set { patientId = value; }
		}

		public Decimal PatientTemp {
			get { return patientTemp; }
			set { patientTemp = value; }
		}

		public String TemperatureUnit{
			get { return temperatureUnit; }
			set { temperatureUnit = value; }
		}

		public String TemperatureLocation{
			get { return temperatureLocation; }
			set { temperatureLocation = value; }
		}

		public TempSubject(){
		}
		public TempSubject(Decimal id){
			patientId = id;
		}
	}



	enum TempUnit
	{
		C,
		F
	}
}

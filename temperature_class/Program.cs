using System;
using System.Collections.Generic;
using System.Text;

namespace temperature_class
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// variables for validation
			Boolean numOnly = false; // value to see if ID is numeric only. Gets set to true if validation is positive
			Boolean correctTemp = false; // value to see if temp is actually a temp. Gets set to true if validation is positive. 
			Boolean correctUnit = false; // value to see if unit is valid. Gets set to true if validation is positive. 
			Boolean correctSite = false; // value to see if location is present in list. Gets set to true if validation is positive.
			Boolean correctInfo = false; // value to see if user is happy with their inputs. Gets set to true if validation is positive.
			// print main header, then ask for person id to store the information under
			while (correctInfo == false) {
				LineOutputs.MainHeader ();
				LineOutputs.EnterPersonId ();
				// validate the person id as only containing numeric chars. will continue until it gets numeric only.
				var unvalidatedId = Console.ReadLine ();
				numOnly = Validators.validateId (unvalidatedId);
				while (numOnly == false) {
					LineOutputs.InvalidId ();
					unvalidatedId = Console.ReadLine ();
					numOnly = Validators.validateId (unvalidatedId);
				}
				var validatedId = Convert.ToDecimal (unvalidatedId);
				// if successful, escape while, create new tempsubject to put the information in
				var subject = new TempSubject (validatedId);


				// ask for temperature
				LineOutputs.EnterTemp ();
				var unvalidatedTemp = Console.ReadLine ();
				correctTemp = Validators.validateNumeric (unvalidatedTemp);
				while (correctTemp == false) {
					LineOutputs.InvalidTemp ();
					unvalidatedTemp = Console.ReadLine ();
					correctTemp = Validators.validateNumeric (unvalidatedTemp);
				}
				var validatedTemp = Convert.ToDecimal (unvalidatedTemp);
				// dump into object
				subject.PatientTemp = validatedTemp;


				// ask for temp unit
				LineOutputs.EnterUnits ();
				var unvalidatedUnit = Console.ReadLine ();
				correctUnit = Validators.validateUnits (unvalidatedUnit);
				while (correctUnit == false) {
					LineOutputs.InvalidUnit ();
					unvalidatedUnit = Console.ReadLine ();
					correctUnit = Validators.validateUnits (unvalidatedUnit);
				}
				var validatedUnit = unvalidatedUnit;
				// dump into object
				subject.TemperatureUnit = validatedUnit;

				// ask for site
				LineOutputs.EnterSite ();
				LineOutputs.PrintSiteMenu ();
				var unvalidatedSite = Console.ReadLine ();
				correctSite = Validators.validateSite (unvalidatedSite);
				while (correctSite == false) {
					LineOutputs.InvalidSite ();
					unvalidatedSite = Console.ReadLine ();
					correctSite = Validators.validateSite (unvalidatedSite);
				}
				var validatedSite = LineOutputs.sites [(Convert.ToInt32 (unvalidatedSite) - 1)];
				// dump into object
				subject.TemperatureLocation = validatedSite;

				// Print object properties
				//http://stackoverflow.com/questions/4023462/how-do-i-automatically-display-all-properties-of-a-class-and-their-values-in-a-s
				LineOutputs.PrintProperties (subject);

				// confirm that this is the correct info
				LineOutputs.ConfirmInputs ();
				var unvalidatedInfo = Console.ReadLine ();
				correctInfo = Validators.validateInputs (unvalidatedInfo);
			}

			// write out or some crap.

			Console.WriteLine ("Thanks!");

		}
	}
	/// <summary>
	/// Validators. Used to make sure user input does not contain incorrect data that could corrupt the data set.
	/// </summary>
	class Validators
	{
		public static Boolean validateInputs(string text){
			switch (text) {
			case "y":
			case "Y":
			case "yes":
			case "Yes":
				return true;
			case "n":
			case "N":
			case "No":
			case "no":
				return false;
			default:
				LineOutputs.printInvalid ();
				return false;
			}
		}

		public static Boolean validateSite(string text){
			// TODO: need to validate that text only has numeric chars
			var upperBound = (LineOutputs.sites.GetUpperBound (0) + 1);
			var lowerBound = 1;
			var boundTest = Convert.ToInt32 (text);
			if (boundTest > upperBound || boundTest < lowerBound)
				return false;
			else
				return true;

		}

		public static Boolean validateId(string idText){
			foreach (char c in idText) {
				if (Char.IsLetter (c) || Char.IsPunctuation (c) || Char.IsWhiteSpace (c) || Char.IsControl (c))
					return false;
				else
					continue;
			}
			return true;
		}

		public static Boolean validateNumeric(string text){
			var cCount = 0;
			foreach (char c in text) {
				if (Char.IsDigit(c) || Char.IsNumber(c) || c == '.')	// add validation to make sure there's only one decimal
					cCount++;
			}
			if (cCount == text.Length)
				return true;
			else
				return false;
		}

		public static Boolean validateUnits(string text){
			if(text == "f" || text == "F" || text == "c" || text == "C" )
				return true;
			else
				return false;
			}
		}
	}

	/// <summary>
	/// Base class that contains Dashes(), making it to where one need not include it in 
	/// printed message methods.
	/// </summary>
	class OutputStrings
	{
		protected static void Dashes()
		{
			for (int i = 1; i < 81; i++){
				Console.Write("-");
			}
			Console.Write("\n");
			return;
		}
		public static void printInvalid(){
			Console.WriteLine ("I'm sorry, that's an invalid input");
			return;
		}
	}


	/// <summary>
	/// The "View" for the application. Inherits from Separator for its Dashes() method.
	/// </summary>
	class LineOutputs : OutputStrings
	{
		public static string[] sites = { "Oral", "Temporal", "Tympanic", "Axillary", "Rectal" };	// http://adctoday.com/learning-center/about-thermometers/how-take-temperature
		public static void MainHeader(){
			Console.WriteLine ("Welcome to the temperature program.");
			Dashes ();
			return;
		}
		public static void ConfirmInputs(){
			Console.WriteLine ("Are these the values you wish to record?(y/n): ");
			return;
		}
		public static void InvalidUnit(){
			Console.WriteLine ("I'm sorry, that isn't a valid unit.");
			Dashes ();
			return;
		}
		public static void InvalidTemp(){
			printInvalid ();
			Console.WriteLine ("Enter only an integer or decimal value");
			Dashes ();
			return;
		}
		public static void InvalidId(){
			printInvalid ();
			Console.WriteLine ("Enter only numbers, no symbols or letters). Please try again: ");
			return;
		}

		public static void EnterTemp(){
			Console.WriteLine ("Please enter a temperature, up to hundredth of a degree: ");
			Dashes ();
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

		public static void InvalidSite(){
			Console.WriteLine ("I'm sorry, that isn't a valid site from the menu.");
		}
		
		public static void PrintProperties(TempSubject sub){
			var props = sub.GetType ().GetProperties ();
			foreach (var p in props)
			{
				Console.WriteLine (p.Name + ": " + p.GetValue (sub, null));
			}
			return;
		}
		public static void PrintSiteMenu(){
			
			for (int i = 0; i < sites.Length; i++) {
				Console.WriteLine ((i + 1) + ". " + sites [i]);
			}
		}
	}
	

	/// <summary>
	/// The model for the data. Acts as the "bucket" for our data.
	/// </summary>
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
		//constructor(s):
		public TempSubject(Decimal id){
			patientId = id;
		}
	}

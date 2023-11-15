//WORKOUT SECTION

24-05-23
SECTION 2 & 3

implicit and explicit conversion
int, float, double, long 

float x = 31.25f;
long y = (long)x;
Console.WriteLine(y); //31

long x = 312145;
int y = (int)x;
Console.WriteLine(y); //312145

long x = 312145;
float y = (float)x;
Console.WriteLine(y); //312145

convert int to long 
convert float to double 
 
convert long to int 
convert double to float 

-----------------
int x = 445;
string y = (string)x;
Console.WriteLine(y);
//cannot convert int to string --use variable.ToString()

int x = 445
string y = x.ToString();
Console.WriteLine(y);

----------------
string x = "445";
int y = (int)x;
Console.WriteLine(y);
//cannot convert string to int --use int.Parse(x) or Int32.Parse(x)

string x = "445";
int y = int.Parse(x);
Console.WriteLine(y);

//STRING MANIPULATION

using concatenatin 
string formatting - uses indexes "hi hello {0}", variableName
string manipulating - $"hi hello {variableName}"
vibating string - @ "hi \n jack \d" --cannot work 

//VARIOUS STRING METHODS - 5

Substring (startingPositin, noOfCharacterPrint )
ToLower();
ToUpper();
Trim();
IndexOf('singleCharacter') -- 0, 1,2,3 



//STRING.FORMAT EXPLAINED

-- Assigning a value in RunTime 

Console.Write("Enter a string: ");
string a = Console.ReadLine();

string x = String.Format("Hi hello {0}", a.Substring(2, 4)); //Ragavan o/p-- Hi hello gava
Console.WriteLine(x);

//EXAMPLE STRING.FORMAT
//STRING EXERCISE
//DATATYPES AND VARIABLES


//CONSTANT

const string pi = 3.14546877;
pi = 1.6; -- error 






//INTRO TO FUNCTION/METHODS

Method is code block inside sequence of statements will run
A program causes the statements to be executed by calling the method 
A main method is the entry point for every C# application 

syntax 

accessSpecifier 	type 	MethodName	()
{
	//code 
}

static void Main(string[] args)


//VOID METHODS

If you use datatype in method function must mention return in code 
If we mention void type we dont mention return 

//METHODS WITH RETURN VALUE AND PARAMETERS


static void Main(string[] args)
{
	Console.WriteLine(add()); //6
	
}


Public static int add()
{
	int x = 2;
	int y = 4;
	int z = x+y;
	return z;			
}
	
----
static void Main(string[] args)
{
	Console.WriteLine(add(4,4)); //8
	
}


Public static int add(int x, int y)
{
	return x + y;			
}
-------
//SOLUTION FOR METHOD
//USER INPUT


static void Main(string[] args)
{
	string x = "Ragavan";
	string y = "Parthi";
	string z = "Vicky";
	friendName(x,y,z);
	
}


Public static void friendName(string a, string b, string c)
{
	Console.WriteLine("Hi Hello {0}",a);
	Console.WriteLine("Hi Hello {0}",b);
	Console.WriteLine("Hi Hello {0}",c);
				
}

//TRY CATCH FINALLY
--Error handling 
--Pre-defined Exception 

ex.
	int x = 5;
	int y = 0;
	int z = x / y;
	Console.WriteLine(z); //Error - DivideByZeroException


try
{
    int x = 5;
    int y = 0;
    int z = x / y;
    Console.WriteLine(z);
}
catch (DivideByZeroException)
{

    Console.WriteLine("Cannot divide by zero");
}

//OPERATORS

Increment, Pre-Increment, Decrement, Pre-Decrement operator 

x++ ++x x-- --x 

eg.

x=0;
x++; --add by 1 
Console.WriteLine(x++); --1
---------
int x=0;
Console.WriteLine(++x); //1
Console.WriteLine(++x); //2
Console.WriteLine(x++); //2
Console.WriteLine(x); //3

int x=0;
Console.WriteLine(--x); //-1
Console.WriteLine(x--); //-1
Console.WriteLine(x); //-2
Console.WriteLine(--x); //-3

ADD, SUB, MULTIPLY, DIVIDE, MODULUS 

RELATIONAL OPERATORS //true or false
<	>	<=	>=

EUQALITY OPERATORS //true or false
==	!=

LOGICAL OPERATORS 
&& || AND OR 
BOTH CONDITION TRUE IS TRUE  //AND &&
ANY ONE CONDITION TRUE IS TRUE  //OR ||

==================================

25-05-2023

SECTION 4

Making Decisions Intro 

IF IF ELSE IF ELSE IF ELSE

if (condition)
{
	//code
}
else if
{
	//code
}
else
{
	//code
}
---------------------
Decision Making In C#

int x = 5;

if (x <10)
{
	Console.WriteLine("x is less than 10");
}
else if (x>=10 and x<=30)
{
	Console.WriteLine("x is greater than or equal to 10 and x is less than or equal to 30")
}
else 
{
	Console.WriteLine("x is greater than 30")
}
----------------
//Tryparse() Method

use TryParse when convert from string to int 

Console.WriteLine("Enter a number");
string x = Console.ReadLine();
int intName;
bool success = int.TryParse(x, out intName);

Console.WriteLine ("The entered number is "+intName); //45  //abc
Console.WriteLine("The bool value is "+success);  //true  //false
----------------

//If And Else If + Tryparse()

Given value is true the output will execute 
Given value is false error will throw so we can handle the error by
using TryParse() 

TryParse - can assign to bool only true or false 
write if statement for TryParse - assVar is true then assign the value 
(two variable - true means callVar = assVar; false means callVar = 0;)

eg.

Console.WriteLine("Enter a number");
string x = Console.ReadLine();
int y;
int z;

if (int.TryParse(x, out y))
{
    z = y;
}
else
{
    z = 0;
    Console.WriteLine("Enter Incorrect format, value set to 0");
}

if (z >30)
{
    Console.WriteLine("x is greater than 30");
}
else if (z >= 10 && z<= 30)
{
    Console.WriteLine("x is greater than or equal to 10 and x is less than or equal to 30");
}
else
{
    Console.WriteLine("default set to 0");
}

------------------------
//Nested If Statements

Nested if - if condition true then call inside if statement it is true then it call inside if statement 



Console.WriteLine("Enter your age");
string stringAge = Console.ReadLine();
int age;
int enteredAge;
bool boolAge = int.TryParse(stringAge, out enteredAge);

if (boolAge)
{ 
    age = enteredAge;
}
else
{
    age = 0;
    Console.WriteLine("Enter your age in number otherwise age set to 0");
}

if (age >= 3 && age <= 70)
{
    Console.WriteLine("This age can enter into mall");
    if (age >= 18 && age <= 70)
    {
        Console.WriteLine("This age can enter into mall and cinema");
        if (age >= 24)
        {
            Console.WriteLine("This age can enter into mall, cinema and party hall");
        }
    }
}
else
{
    Console.WriteLine("This age is not allowed");
}

--------------------

//Solution For The Challenge - If Statement

Coding Nested If Statement
Switch Statement


int age = 10;

switch(age)
{
	case 10:
		Console.WriteLine("can play");
		break;
	case 20:
		Console.WriteLine("can play");
		break;
	default:
		Console.WriteLine("can play");
		break;
	
}

-----------------
//Challenge If Statement --Checkhighscore

global variable;

main method we should call the method CheckHighScore

CheckHighScore(int score, string playerName)

if (score> highScore)
{
	highScore = score;
	playerName = HighScorePlayerName;
	Console.WriteLine("The high score is and the player name is ");
}
else 
{
	Console.WriteLine($"The old high score is {highScore} and the player name is {playerName} record cannot break");
	
}
-------------
Enhanced If Statements - Ternary Operator

	condition ? expression 1 : expression 2;

Challenge - Ternary Operator 

string to int 

number to int is execute 
string to int is error throw
use parse - error  
use TryParse to handle error 
assign bool and check true means ternary operator and false means go to else part & print invalid temp

class program
{
    static void Main(string[] args)
    {
        int inputTemperature = 0;
        string temperatureMessage = string.Empty;
        string inputValue = string.Empty;

        //takes input from console 
        Console.WriteLine("Enter the current temperature: ");
        inputValue = Console.ReadLine();

        //Validate the input as valid input integer value 
        bool validInteger = int.TryParse(inputValue, out inputTemperature);

        if (validInteger)
        { //condition ? iftrue : iffalse
            //if it is valid integer then it is check for the conditions using 
            temperatureMessage = inputTemperature <= 15 ?
                //true
                "it is too cold here" :
                //false
                (inputTemperature >= 16 && inputTemperature <= 28) ?
                //true
                "it is cold here" :
                //false
                inputTemperature > 28 ?
                //true
                "it is hot here" :
                //false
                "";
            Console.WriteLine(temperatureMessage);
        }
        else
        {
            //in case if the input value is not valid temperature 
            Console.WriteLine("Not a valid temperature");
        }
        Console.ReadKey();
    }
}

------------------------
=============================

26-05-2023

//LOOPS INTRO

types of loops and uses 

//FOR LOOP

for (input; condition; increment)
{
	//code
}

//DO WHILE

Execute the code and check while 

do{
	int i = 0;
	i++;
	Console.WriteLine(i);
	
}while(i<10);
	

//WHILE LOOP 

while (i<10)
{
	int i = 0;
	Console.WriteLine(i);
	i++;
	
}
//EXERCISE LOOP 

odd numbers only 

for(i = 1; i<20; i+=2 )
 {
	Console.WriteLine(i);
}


//BREAK AND CONTINUE

break - It exit from the code 
continue - It will skip the code what we mention in if statement

//CHALLENGE LOOP - STUDENT SCORE 

//globally variable 

int totalAvg = 0; //increment 
int avgScore = 0; //input score add to this variable 
int score =0;

while (score != -1)
{
    Console.WriteLine("enter a student score or enter -1 to enter");
    score = int.Parse(Console.ReadLine());

    if (score >= 0 && score <= 20)
    {
        avgScore += score;
        totalAvg++;
    }
    else
    {
        if(score > 21)
       Console.WriteLine("Please enter student score range of 0 to 20");
        else
            Console.WriteLine("");

    }

}
Console.WriteLine("The Average core for students is "+ avgScore/totalAvg);

10 not equal to -1  -- true 
-1 not equal to -1 --false 



1<10 -- true 
10<10 -- false 



SECTION - 6

//OBJECTS INTRO 
//INTRODUCTION TO OBJECTS AND CLASSES
//OUR FIRST OWN CLASS

two class in one project 

call human class in program class to execute 

create variable and method in public only can call 

human class 

public string firstName;
public string lastName;

public static void names()
{
	Console.WriteLine($"Hi my name is {firstName} {lastName}");
}

program class 

static void Main(static[] args)
{
	human callProgram = new human();
	callProgram.firstName = "Ragavan";
	callProgram.lastName = "Ks";
	
	Console.WriteLine(callProgram);
}

--------------------

30-05-23

Using Constructors

	We use a class name in public name called constructors also reduce the code lines
	we can pass parameters in a public name 
	eg. class name box 
	public box (int l, int w, int b)
	exec - box total = new box(10,20,30);
	
Mutliple Constructors

	We can create mutltiple constructors and create 2 constructor for that also write if else in method
	eg. firstName, lastName, age

	//constructor 
	public details (string firstName)
	{
		this.firstName = firstName;
	}

	//method 
	public void employeeInfo()
	{
		Console.WriteLine("Hi My name is {0}", firstName);
	}


Exercise Constructors --Phone 

	create constructor and method as per the constructor arguments 

Access Modifiers 

public, private, protected 

Properties Part 1

	private variable 
	public method la call & assign (input variable will assign to private variable)
	public method la private variable can return & print 
	eg. get - return , set - variable assign 
	can create a two method for get and set 

Properties Part 2


/*
--method 

set value 

public void name (string name)
{
	this.name = name;
}

get value 

public string name ()
{
	return name;
}

--property 

set value 

public void setLength(int length)
{	
	this.length = length;
}

public void getLength()
{
	return length;
}
*/
class Person
{
  private string name; // field

  public string Name   // property
  {
    get { return name; }   // get method
    set { name = value; }  // set method
  }
}

--we can directly get & set 
	public int volume 
	{
		get {return l*h*w;}
		set {this.volume = value;}
	}

--private variable -print

	property or method ???
	
method 
public string name()
{
	return this.name;
}

property
public string name 
{
	get {return this.name;}
	set {this.name = value;}
}

------------------------
Challenge Properties
Members & Finalizers/Destructors
--------------------
31-05-23

Foreach Loops 

array - for loop 

int[] x = new int[5];

for (int i = 0; i < 5; i++)
{
    x[i]=i;
}

for (int j = 0; j < 5; j++)
{
   Console.WriteLine("The index {0} element is {1}", j, x[j]);
}



foreach (int k in x)
{
    Console.WriteLine(k);
}
------------------------
Why Foreach?
Exercise: Arrays, for & foreach loops
challenge: Foreach Loops & Switch statement loops 

true or false 

1 - string 
2 - integer 
3 - boolean 

1 - input true - string type //if 
1 - input false - string type //else


---------------------

bool valid = false;
string inputType;
	
console...(Enter any value)
string input = console.ReadLine();

console...(press 1,2,3 to confirm the type) //int
int inputType = console.ReadLine();

switch (input)
{
	case 1:
		valid = string method call //true or false 
		inputType = string;
	case 2:
		int y = 0;
		valid = int.TryParse(input, out y); //true or false
		inputType = int;
	case 3;
		bool z = false;
		valid = bool.Tryparse(input, out z); //true or false
		inputType = boolean;
}
console....(input value)
if (valid)
{
	Console....(Enter value is valid: stringType);
}
else 
{
	console.....(Enter value is invalid: stringType);
}



--------------------------------
Multi Dimensional Arrays 

int[,] array2D = { {1,2,3},{4,5,6}}

int[,,] array3D = 
{ 
	{ 
		{1,2,3},{4,5,6}
	},
	{ 
		{11,12,13},{14,15,16}
	},
	{ 	
		{21,22,23},{24,25,26}
	} 
};

-------------------------------------------
01-06-2023

//NESTED FOR LOOPS AND 2D ARRAYS 

array = 1,2,3,4...10
print 1 2 3 4 5 ...10 using for loop & foreach loop 

int[,] number = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        //nested for loop
        //outer for loop 
        for (int i = 0; i < number.GetLength(0); i++)
        {
            for (int j = 0; j < number.GetLength(1); j++)
            {
                Console.Write(number[i, j] + " ");
            }
        }



//NESTED FOR LOOPS AND 2D ARRAYS - TWO EXAMPLES

odd number even number diagonal

int[,] number = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        //nested for loop
        //outer for loop 
        for (int i = 0; i < number.GetLength(0); i++)
        {
            for (int j = 0; j < number.GetLength(1); j++)
            {
				if(number%2 	== 0)
				{
                Console.Write("Even numbers: " + number[i, j] + " ");
				}
				else if (number%2 == 1)
				{
				Console.Write("Odd numbers: " + number[i, j] + " ");
				}
			}
        }
		
		
		


//CHALLENGE - TIC TAC TOE

display setField 
player 1: choose a field 
given input check if already taken or not 
if taken return error or else that number will change to player 1 sign
then check if player sign is win or not
if not means 
player 2: choose a field 

orderwise 
1. setField method 
2. player 1 choose a field 
	enterXorO method - player 1 sign O , p2 sign X - input check switch 
3. check given input taken or not - error throw if enter already taken number  
4. check player 1 sign is matched or not elseif throw 10 times 
	player 1 has won - press any key to restart 
	resetField method - copy setField & assing to setField & throw = 0
5. 


int player = 2;
int throw = 0;

//JAGGED ARRAYS	

int[][] jA = {new int[]{1,2,3}, new int[] {4,5,6}, new int[] {7, 8}};

print 0,0  0,1   0,2
	  1,0  1,1   1,2
	  2,0  2,1   2,2
	  
for loop i = 0 3 times run //outer loop 0, 1, 2
for loop j = 0 3 times run  //inner loop 0,1,2   0,1,2    0,1,2

for (int i = 0; i < jA.Length; i++) //length - 3
{
	for (int j = 0; j < jA[i].Length; j++) // 0 length - 3, 1 length - 3, 2 length - 2
	{
		Console.WriteLine(jA[i][j]);
	}
}


//JAGGED ARRAYS OR MULTI DIMESIONAL ARRAY 

//CHALLENGE JAGGED ARRAYS
//USING ARRAYS AS PARAMETER

array
average value 

int[] x = {1,2,3,7}

static bool getAvg(int[] input)
{
	foreach ()
}

-------------------------------------
//HASHTABLE CHALLENGE 



class program 
{
	static void Main(string[] args)
	{
	Hashtable table = new Hashtable();
	
	students[] stuDetails = new student(1, "Akash", 85);
	students[] stuDetails = new student(2, "Benny", 95);
	students[] stuDetails = new student(3, "Chandran", 76);
	students[] stuDetails = new student(1, "David", 63);
	students[] stuDetails = new student(4, "Ganesh", 58);
	
	foreach (students s in stuDetails)
	{
		if(!table.ContainsValue(s.id))
		{
			table.Add(s.id, s);
			Console.WriteLine("This id {0} is updated successfully", s.id);
		}
		else
		{
			Console.WriteLine("This id {0} is already exist", s.id);
		}
	}
	
	
	}
}
class students 
{
	public int id { get; set; }
	public string name { get; set; }
	public int gpa { get; set; }
	
	public void student(id int, name string, gpa int)
	{
		this.id = id;
		this.name = name;
		this.gpa = gpa;
	}
	
}


//DICTIONARIES 

class program
{
	static void Main(string[] args)
	{
		Employee[] emp = 
		{
		new Employee("CEO", "Gwyn", 95, 200),
		new Employee("Manager", "Joe", 35, 25),
		new Employee("HR", "Lora", 32, 21),
		new Employee("Secretary", "Petra", 28, 18),
		new Employee("Lead Developer", "Artorias", 55, 35),
		new Employee("Intern", "Ernest", 22, 8) 

		Dictionary<string, Employee> empDirectory = new <string, Employee>();
	
		foreach(Employee e in emp)
		{
			empDirectory.Add(e.role, e);
		}
		
		for (i = 0; i < empDirectory.Count; i++)
		{
			KeyValuePair <string, Employee> keyValue = empDirectory.ElementAt[i];
			
			Console.WriteLine ("Key: {0}, i is: {1}", keyValue.Key, i);
			
			
		}


	}
}


class Employee 
{
	public string role { get; set; };
	public string name { get; set; };
	public string age { get; set; };
	public string rate { get; set; };
	public float salary 
	{ 
	get {return rate*8*5*4*12}; 
	};
	
	public Employee (string role, string name, string age, string rate)
	{
		this.role = role;
		this.name = name;
		this.age = age;
		this.rate = rate;
		
	}
	
}


//DICTIONARY CONCEPT 

	DICIONARY SYNTAX 
	
	DICTIONARY <Tkey, Tvalue> varName1 = new dictionary <Tkey, Tvalue>();

Stored all variable & constructor to call & assign that variable in class 

	className[] varName2 = 
	{
		varName[0] = new constructorName (Parameter1, parameter2,....);
	};
	
	//adding collection to dictionary
	foreach (className varName3 in varName2)
	{
		//dictionary.Add(Tkey, Tvalue);
		varName1.Add(varName3.Role, varName3);
	}
	
	//print 
	key value pair keyword - assign it by ElementAt()

	many indexes in collection 
	so run it in loop 
	
	for (i = 0; i < dictVarName.Count; i++)
	{
	KeyValuePair <string, Employee> KeyValueName = dictVarName.ElementAt(i);
	
	In KeyValueName will stored the Key & Value
	
	//We can print key name by KeyValueName.Key 
	
	//We can print value name by KeyValueName.Value 
	//Here value has more than one so call it by KeyValueName.Value.FieldName 
	// or assign to collection variable = KeyValueName.Value --it will stored all value inside 
	
	//Print it by collVar.FieldName 
If we known the key value we can print it by two methods 

if (dictName.ContainsKey(keyName) )
	Collection name = dictName[keyName]
	print collName.fieldName 

collName varName = null;
if (dictName.TryGetValue("KeyName", out varName))
	print it by varName.fieldName 
	
	
	
	}
//EDITING AND REMOVING ENTRIES IN DICTIONARY
	
	UPDATE REMOVE 
	
	UPDATE - ContainsKey()
	dictName[KeyName] = new Employee(parameter1, para2,....);
	
	REMOVE - dictName.Remove(keyName)

//STACK AND QUEUE 
//STACK 
	
	Push();
	Pop();
	Peek();
	
	syntax:
		stack<int> name = new stack<int>();
		
		name.Push(1);
		name.Push(2);
		//print name.Peek()
		//Remove 
		name.Pop();
		
		int[] num = {1,2,3,4};
		
		stack<int> myStack = new myStack<int>();
		
		foreach (int i in num)
		{
			myStack.Push(i);
		}

		Remove 
		while(myStack.Count < 0)
		{
			int new = myStack.Pop();
			Console...(new);
		}
		
//QUEUES

	Enqueue  Dequeue  
	first in first out 

--
order id order quantity 
public variable 
public constructor 
two method 
variable1
variable2

receive order 1 
variable 1 foreach run & add to queueName 

receive order 2 
same follows as 1 

main method - call queue in foreach & queueName.CurrentProcess(); 
current process - order id will print 

-----------------------
//DEBUGGING BASICS
Break point - f9 
step next f11 
step over f10 

//LOCALS AND AUTOS
we can see the method constructor in the locals and autos 

//DEBUGGING, CREATING COPIES OF LISTS AND SOLVING SOME BUGS 
//DEBUGGING CALL STACK, THROWING ERRORS AND DEFENSIVE PROGRAMMING 

SECTION - 9: INHERITANCE AND MORE ABOUT OOP 

//INTRODUCTION TO INHERITANCE
	we can derived class from another class 
	code reuseability 
	reduce the size of program 

//SIMPLE INHERITANCE EXAMPLE 
create 4 classes - radio, tv, electrical device, program 
electrical device - parent 
store public vairable 
radio, tv - child 
radio : electrical device 

---
public variable - parent 
public constructor : base(parameter 1, parameter 2...) - child

we can call it electrical device methods via child class 

//VIRTUAL AND OVERRIDE KEYWORDS

create a method 
class 1
public virtual void name1()

class 2
public override void name1()

if you want to call class 1 
base.name1();
if you want to call class 2 
name1();

12-06-23
//Read from a text file 

	string text = System.IO.File.ReadAllText(@"location path");
	console...(x);
	
	string[] lines = System.IO.File.ReadAllLines(@"location path");
	
	foreach(string y in x)
	{
	console...(y);
	}
-----------------
//Write a text into file 

string line = "line 1";

File.WriteAllText(@"locationPath", line)

string[] lines = {"l1", "l2", "l3"};

File.WriteAllLines(@"locationPath", lines)

//Add text 
	
	using (StreamWriter file = new StreamWriter(@"locationPath.newfilename"))
	{
		foreach (string line in lines)
		{
			if(line.Contains("2"))
			{
				file.WriteLine(line)
			}
		}
	}

//Add text to exisiting file 

	using (StreamWriter file = new StreamWriter(@"locationPath.exisitingFileName, true"))
	{
				file.WriteLine("Add some lines")
	}

==========================
16-06-23

//EXERCISE DELEGATES
//ANONYMOUS METHODS


	delegate keyword and define by using delegate variable type 
	
	delName name = delegate(delegatetype name) { if (condition){return true}}

//LAMBDA EXPRESSIONS

	=>
	expression lambda
	statement lambda 
	
	p => p.age == 25 
	
	p = > if (p.age > 25 && p.name.contains("S")) {return true ; else tre}


//EXERCISE 15: LAMBDA EXPRESSIONS
//EVENTS AND MULTICAST DELEGATES

	create a delegate 
	use delegate name to create a two variable name & must give this name as event
	
	public delegate void delName ();
	
	public static event delName eventName1, eventName2;

	use this two event name to other classes like 
	
	constructor 
	
	public className ()
	{
		GameEvent.eventName1 += Method1;
		GameEvent.eventName2 += Method2;
	}

	GameEvent class -
		create a method to trigger the event 
			public static trigger1
			{
				if(eventName1 != null)
				{
					cw("Game starts");
					
					eventName1();
				}
			}
			
			same for trigger2 
			
			trigger will be handled in execute program 
			
				class program 
				
					create panna class ku lam object create pannita podhum event la poi adhoda method lam store aagidum 
					
					namba trigger method call panna podhum 
						gameEvent => trigger1 => event1 => all class liyum poi assign panni irupom andha method call agum => cw(lines display)





//DELEGATES OUTRO
 





























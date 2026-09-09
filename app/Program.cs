namespace app
{
    #region question 1
    /*
    
    A:
   
    Method overloading
    repeating same function name while changing paramter types , paramter number or paramter order 
     
    Method overriding:
    
    repeating same function name in child while it exists in the parent,
     keyword "override" indicate that same function would have different implmentation in chilld


    B:
    static binding:
    1-happen in overloading
    2-data is binded during compile time

    dynamic bidning:
    1-happen at run time
    2-data is binded during runtime 
     */

    #region question 2
    /*
    
    1-sealed keyword : indicates that the class wouldn't be inherited 

    2-
    sealed class can't be inherited from other childs
    sealed methods can't be overriden from other childs

    3-sealed methods can't be overriden because it keeps the implementation of method from any change 

    

     */


    #endregion

    #endregion
    internal class Program
    {

        public struct DeliveryAddress
        {
            string city;
            string street;
            int BuildingNumber;

            public DeliveryAddress(string c, string st, int BN)
            {
                city = c;
                street = st;
                BuildingNumber = BN;
            }

            public string GetFullAddress()
            {
                return $"Full Address = {city},{street},{BuildingNumber}";
            }
        }

        public static class DeliveryHelper
        {
           public static void printShipments(Shipment shipment)
            {
                shipment.PrintShippment();
            }
    
        }

        public class Shipment
        {
            private string trackingCode;
            private string description;
            private float weight;
            private decimal deliveryFee;

            public string TrackingCode
            {
                get
                {
                    return trackingCode;
                }


                set
                {
                    if (value is not null && !(value == ""))
                    {
                        trackingCode = value;
                    }
                }

            }

            public string Desciption
            {
                get
                {
                    return description;
                }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        description = value;
                    }
                }
            }

            public float Weight
            {
                get { return weight; }

                set
                {
                    if (value > 0)
                    { weight = value; }
                }


            }

            public decimal DeliveryFee
            {
                get { return deliveryFee; }
                set { if (value > 0) { deliveryFee = value; } }
            }

            public DeliveryAddress Destination { set; get; }
            public virtual decimal EstimatedCost { get { return deliveryFee + (decimal)weight * 5; } }



            public Shipment(string trackingCode)
            {
                this.trackingCode = trackingCode;
                weight = 1;
                description = "UnKnown";
                deliveryFee = 50;
                Destination = new DeliveryAddress("cairo", "haron", 12);
            }

            public Shipment(string trackingCode, string description, float weight, decimal deliveryFee, DeliveryAddress destination)
            {
                this.trackingCode = trackingCode;
                this.description = description;
                this.weight = weight;
                this.deliveryFee = deliveryFee;
                this.Destination = destination;

            }

            public void updateWeight(float weight)
            {
                this.weight = weight;
            }

            public void updateWeight(float weight,float packageWeight)
            {
                this.weight = weight + packageWeight;

            }
            public void updateDeliveryFee(decimal newFee)
            {
                if (newFee > 0)
                    deliveryFee = newFee;
            }

            public virtual void PrintShippment()
            {

                Console.WriteLine($"Shippment information:\n" +
                    $"tracking code={trackingCode}\n" +
                    $"weight={weight}\n" +
                    $"description={description}\n" +
                    $"delivery fee={deliveryFee}\n" +
                    $"Destination={Destination.GetFullAddress()}\n");
            }
           

        }


        public class StandardShipment : Shipment
        {

            public StandardShipment(string trackingCode) : base(trackingCode)
            { }

            public StandardShipment(string trackingCode, string description, float weight, decimal deliveryFee, DeliveryAddress destination)
                : base(trackingCode, description, weight, deliveryFee, destination)
            { }



            override public void PrintShippment()
            {

                Console.WriteLine($"Standard Shippment information :\n" +
                    $"tracking code={TrackingCode}\n" +
                    $"weight={Weight}\n" +
                    $"description={Desciption}\n" +
                    $"delivery fee={DeliveryFee}\n" +
                    $"Destination={Destination.GetFullAddress()}\n");
            }


        }


        public class ExpressShipment : Shipment
        {
            public decimal ExtraFree { get; set; }
            public override decimal EstimatedCost { get { return DeliveryFee + (decimal)Weight * 5 + ExtraFree; } }
            public ExpressShipment(string trackingCode) : base(trackingCode)
            { }

            public ExpressShipment(string trackingCode, string description, float weight, decimal deliveryFee, DeliveryAddress destination, decimal extraFree)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
                ExtraFree = extraFree;
            }



            override public void PrintShippment()
            {

                Console.WriteLine($"express Shippment information :\n" +
                    $"tracking code={TrackingCode}\n" +
                    $"weight={Weight}\n" +
                    $"description={Desciption}\n" +
                    $"delivery fee={DeliveryFee}\n" +
                    $"extra={ExtraFree}"+
                    
                    $"Destination={Destination.GetFullAddress()}\n") ;
            }

            public void GenerateCustomsReport()
            {

            }

        }


        public class internationalShipment : Shipment
        {
            public string DestinatinoCountry
            {
                get { return DestinatinoCountry; }

                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        DestinatinoCountry = value;


                }

            }

            protected decimal customfee;
            public decimal CustomFee
            {
                get { return customfee; }
                set
                {
                    if (value > 0)
                    { customfee = value; }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            public override decimal EstimatedCost { get { return DeliveryFee + (decimal)Weight * 5 + CustomFee; } }

            public internationalShipment(string trackingCode) : base(trackingCode)
            { }

            public internationalShipment(string trackingCode, string description, float weight, decimal deliveryFee, DeliveryAddress destination, string dest_Country, decimal customFee)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
                CustomFee = customFee;
                DestinatinoCountry = dest_Country;
            }

            public override void PrintShippment()
            {

                Console.WriteLine($"international Shippment information :\n" +
                    $"tracking code={TrackingCode}\n" +
                    $"weight={Weight}\n" +
                    $"description={Desciption}\n" +
                    $"delivery fee={DeliveryFee}\n" +
                    $"extree fees={customfee}"+
                    $"Destination={Destination.GetFullAddress()}\n");
            
            }

            public virtual void GenerateCustomsReport()
            {
                PrintShippment();
            }

        }


        public class PirrorityInternationalShipment :internationalShipment
        {

            public PirrorityInternationalShipment(string trackingCode) : base(trackingCode)
            { }

            public PirrorityInternationalShipment(string trackingCode, string description, float weight, decimal deliveryFee, DeliveryAddress destination, string dest_Country, decimal customFee)
                : base(trackingCode, description, weight, deliveryFee, destination, dest_Country, customFee)
            { }
            
                
            public sealed override void GenerateCustomsReport()
            {
                Console.WriteLine($"Pirrority International Shippment information :\n" +
                                  $"tracking code={TrackingCode}\n" +
                                  $"weight={Weight}\n" +
                                  $"description={Desciption}\n" +
                                  $"delivery fee={DeliveryFee}\n" +
                                  $"extra={customfee}" +

                                  $"Destination={Destination.GetFullAddress()}\n");
            }

           

        }

        
        public sealed class CompleltedShipment : Shipment
        {

            public CompleltedShipment(string trackingCode) : base(trackingCode)
            { }

            public CompleltedShipment(string trackingCode, string description, float weight, decimal deliveryFee, DeliveryAddress destination, decimal extraFree)
                : base(trackingCode, description, weight, deliveryFee, destination)
            { }
               
            
        }
        public class DeliveryCenter
        {
            private Shipment[] Shipments;
            public string CenterName;

            int counter = 0;
            public DeliveryCenter()
            {
                Shipments = new Shipment[20];


            }

            public DeliveryCenter(string name)
            {
                CenterName = name;
                Shipments = new Shipment[20];

            }


            public Shipment this[int index]
            {
                get
                {
                    if (0 <= index && index < counter)
                        return Shipments[index];
                    else return default;
                }

                set
                {
                    if (0 <= index && index < counter)
                        Shipments[index] = value;

                }
            }

            public Shipment this[string index]
            {
                get
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (Shipments[i].TrackingCode == index)
                        {
                            return Shipments[i];
                        }
                    }
                    return default;
                }

                set
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (Shipments[i].TrackingCode == index)
                        {
                            Shipments[i] = value;
                        }
                    }
                }
            }


            public bool AddShipment(Shipment sh)
            {
                if (counter < 10)
                {


                    Shipments[counter] = sh;
                    counter++;
                    return true;
                }
                else
                {
                    return false;

                }
            }

            public bool RemoveShipment(string TrC)
            {
                bool isThere = false;
                int index = -1;
                for (int i = 0; i < counter; i++)
                {
                    if (Shipments[i].TrackingCode == TrC)
                    {
                        isThere = true;
                        index = i;
                    }
                }



                if (isThere)
                {
                    Shipments[index] = Shipments[counter - 1];
                    Shipments[counter - 1] = null;
                    counter--;
                    return true;

                }
                else
                {
                    return false;
                }

            }


            public void PrintALlShipments()
            {
                Console.WriteLine("shipments regestired:");
                for (int i = 0; i < counter; i++)
                {
                    Console.WriteLine(i + 1 + ":");
                    Shipments[i].PrintShippment();
                }
            }

        }


        static void Main(string[] args)
        {
            DeliveryCenter DC=new DeliveryCenter("joy");
            
            StandardShipment standardshippment = new StandardShipment("shoo01", "laptop", 3, 80, new DeliveryAddress("cario", "3bas", 3));
            ExpressShipment expressShippment = new ExpressShipment("sh002", "mobile", 2, 60, new DeliveryAddress("alex", "stanly", 3), 30);
            internationalShipment internationalShipment = new internationalShipment("sh003", "TV", 8, 120, new DeliveryAddress("giza", "faisl", 3), "germany", 100);
            
            DC.AddShipment(standardshippment);
            DC.AddShipment(expressShippment);
            DC.AddShipment(internationalShipment);

            
            Console.WriteLine("============print===========");
            DC.PrintALlShipments();

            Console.WriteLine("=============print using helper=============");
            DeliveryHelper.printShipments(standardshippment);
            DeliveryHelper.printShipments(expressShippment);
            DeliveryHelper.printShipments(internationalShipment);

            Console.WriteLine("========printing after adjusting weight");
            standardshippment.updateWeight(4);
            Console.WriteLine("first overload:");
            standardshippment.PrintShippment();
            Console.WriteLine("second overload");
            expressShippment.updateWeight(4,4);
            standardshippment.PrintShippment();



           

        }
    }
}

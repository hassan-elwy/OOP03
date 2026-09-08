namespace app
{
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

                Console.WriteLine($"Standard Shippment information :\n" +
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
           
           

        }
    }
}

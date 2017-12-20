using System.Collections.Generic;

namespace Vending_Terminal_Software_Classes
{
    public class CurrentProduct : Item
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public string Code { get; set; }
    }

    public class ProductComparer : IEqualityComparer<CurrentProduct>
    {

        public bool Equals(CurrentProduct x, CurrentProduct y)
        {
            //Check whether the objects are the same object. 
            if (ReferenceEquals(x, y)) return true;

            //Check whether the products' properties are equal. 
            return x != null && y != null && x.Code.Equals(y.Code) && x.Name.Equals(y.Name);
        }

        public int GetHashCode(CurrentProduct obj)
        {
            //Get hash code for the Name field if it is not null. 
            int hashProductName = obj.Name == null ? 0 : obj.Name.GetHashCode();

            //Get hash code for the Code field. 
            int hashProductCode = obj.Code.GetHashCode();

            //Calculate the hash code for the product. 
            return hashProductName ^ hashProductCode;
        }
    }
}

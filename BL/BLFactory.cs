using System;

namespace BlAPI
{
    public static class BlFactory
    {
        public static IBL GetBl()
        {
            return new BL.BlImp1();
        }

        public static IBL GetBl(int blType)
        {
            switch (blType)
            {
                case 1:
                    return new BL.BlImp1();
                case 2:
                    return new BL.BlImp1(); // new BL.BlImp2()
                default:
                    throw new ArgumentException("Bad BL number");
            }
        }

    }
}

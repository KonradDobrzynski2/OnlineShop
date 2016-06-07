using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Singletons
{
    public sealed class Singleton_ProductInBasket
    {
        private static Singleton_ProductInBasket m_oInstance = null;
        private static readonly object lock_object = new object();

        private Singleton_ProductInBasket()
        {
            ProductInBasket = new List<Product>();
        }

        public static Singleton_ProductInBasket Instance
        {
            get
            {
                lock (lock_object)
                {
                    if (m_oInstance == null)
                    {
                        m_oInstance = new Singleton_ProductInBasket();
                    }

                    return m_oInstance;
                }

            }
        }

        public List<Product> ProductInBasket;
    }
}

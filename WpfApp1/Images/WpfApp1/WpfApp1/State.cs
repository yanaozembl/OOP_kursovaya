using System;
using System.Collections.Generic;
using WpfApp1.CreateFlat;

namespace WpfApp1
{
    public static class State
    {
        public static Stack<List<Flat>> oldBooks = new Stack<List<Flat>>();
        public static Stack<List<Flat>> newBooks = new Stack<List<Flat>>();
        public static Stack<List<Flat>> Books = new Stack<List<Flat>>();

    }
}
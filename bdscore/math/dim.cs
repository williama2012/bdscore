using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace bds.math {

    public class dim1 : IEnumerator,IEnumerable  {
        int position = -1;
        public d1[] points;

        public IEnumerator GetEnumerator() {
            return (IEnumerator)this;
        }
        public bool MoveNext() {
            position++;
            return (position < points.Length);
        }

        public void Reset() { position = 0; }

        public object Current {
            get { return points[position]; }
        }

    }
    public class dim2 : dim1 {
        public d2[] points;

    }
    public class dim3 : dim2 {
        public d3[] points;

    }




    public class d1  {
        public double x { get; set; }
        public d1() { }
        public d1(double x) { this.x = x; }
    }
    public class d2 : d1    {
        public double y { get; set; }
        public d2() { }
        public d2(double x) { this.x = x; }
        public d2(double x, double y) { this.x = x; this.y = y; }
    }
    public class d3 : d2    {
        public double z { get; set; }
        public d3(double x, double y, double z) { this.x = x; this.y = y; this.z = z; }
    }




}

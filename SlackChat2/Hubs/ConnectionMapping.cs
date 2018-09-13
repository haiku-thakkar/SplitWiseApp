using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackChat2.Hubs
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T,HashSet<string>> _connections=new Dictionary<T,HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public string GetSeparated
        {
            get
            {
                string res = string.Empty;
                for(int i = 0; i < _connections.Count; i++)
                {
                    res += _connections.ToList()[i].Key + ",";
                }
                res = res.TrimEnd(',');
                return res;
            }
        }

        public void Add(T key,string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if(!_connections.TryGetValue(key,out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }
                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }
        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if(_connections.TryGetValue(key ,out connections))
            {
                return connections;
            }
            return Enumerable.Empty<string>();
        }

        public void Remove(T key,string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if(!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
}

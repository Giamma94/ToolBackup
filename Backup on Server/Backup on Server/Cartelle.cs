using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup_on_Server
{
    class Cartelle
    {

        private String dirIniziale = String.Empty;
        private String dirDestinazione = String.Empty;
        private String syncSubDirs = String.Empty;
        public string DirIniziale
        {
            get{ return dirIniziale; }
            set { dirIniziale = value; }
        }

        public string DirDestinazione
        {
            get { return dirDestinazione; }
            set { dirDestinazione = value; }
        }

        public string SyncSubDirs
        {
            get { return syncSubDirs; }
            set { syncSubDirs = value; }
        }
    }
}

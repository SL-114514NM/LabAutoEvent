using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabAutoEvent.API.Featrues
{
    public interface IMiniGameBase
    {
        int GameId { get; set; }
        int GameName { get; set; }
        string GameAuthor { get; set; }
        string GameDescription { get; set; }
        string GameVersion { get; set; }
        bool WillLoad { get; set; }
        void OnEnabled();
        void OnDisabled();

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Entities.DTO
{
    public class CreateUpdateGameDTO
    {
        public CreateUpdateGameDTO()
        {
            Game=new Game();
            GenresDTO = new List<string>();
            ScreenshotsDTO = new List<string>();
            TagsDTO = new List<string>();
            PlatformsDTO = new List<string>();
        }
        public Game Game { get; set; }
        public List<string> GenresDTO { get; set; }
        public List<string> ScreenshotsDTO { get; set; }
        public List<string> TagsDTO { get; set; }
        public List<string> PlatformsDTO { get; set; }

    }
}

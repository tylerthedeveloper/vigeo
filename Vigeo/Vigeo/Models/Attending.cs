namespace Vigeo.Models
{
    public class Attending
    {
        public virtual AllEventsModel AllEventsModel { get; set; }
        public virtual UserModel UserModel { get; set; }
    }

    //public override string ToString() => JsonConvert.SerializeObject(this);
}
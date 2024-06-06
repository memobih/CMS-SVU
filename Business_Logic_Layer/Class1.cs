using Microsoft.AspNetCore.Mvc;


namespace Business_Logic_Layer
{
    public class Class1
    {
        private Data_Access_Layer.Repository.SubjectRepository _subjectRepo;


        public Class1() {
            _subjectRepo = new Data_Access_Layer.Repository.SubjectRepository();
        }

    }
}

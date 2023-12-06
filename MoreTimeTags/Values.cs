namespace MoreTimeTags {
    public class Values {
        public static readonly Values Korean = new Values {
            Credit_Devloper = "개발자",
            Credit_Source = "소스 코드",
            Credit_BugReport = "이 모듈에 대한 버그는 깃허브나 디스코드 'jongyeol_'에게 연락주세요."
        };

        public static readonly Values English = new Values {
            Credit_Devloper = "Developer",
            Credit_Source = "Source Code",
            Credit_BugReport = "Please contact GitHub or Discord 'jongyeol_' for bugs about this module."
        };

        public string Credit_Devloper;
        public string Credit_Source;
        public string Credit_BugReport;
    }
}
using PostSharp.Aspects;
using System;
using System.Security;

namespace DevFramework.core3.Aspects.Postsharp.AuthorizationAspects
{ 
    
    [Serializable]
    public class SecuredOperation:OnMethodBoundaryAspect
    {

        public string Roles { get; set; }

        public override void OnEntry(MethodExecutionArgs args)
        {
            string[] roles = Roles.Split(',');
            bool IsAuthorized = false;

            for (int i = 0; i < roles.Length; i++)
            {
                if (System.Threading.Thread.CurrentPrincipal.IsInRole(roles[i]))
                {
                    IsAuthorized = true;
                }
            }
            if (IsAuthorized==false)
            {
                throw new SecurityException("You are not authrozide!");
            }

        }



    }
}

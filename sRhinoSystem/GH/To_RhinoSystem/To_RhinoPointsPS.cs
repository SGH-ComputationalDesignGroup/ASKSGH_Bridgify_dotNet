﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Grasshopper;
using Grasshopper.Kernel;
using sDataObject;
using sDataObject.sElement;
using sDataObject.sGeometry;
using System.IO;
using sRhinoSystem.Properties;

namespace sRhinoSystem.GH.ToRhinoSystem
{
    public class To_RhinoPointsPS : GH_Component
    {

        public To_RhinoPointsPS()
            : base("sPointSupport to RhinoPoint", "sPointSupport to RhinoPoint", "...", "ASKSGH.Bridgify", "To Rhino")
        {
        }
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; } // | GH_Exposure.obscure;
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("sPointSupport", "sPointSupport", "...", GH_ParamAccess.item);
        }

        public override void CreateAttributes()
        {
            base.CreateAttributes();
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Point", "Point", "Point", GH_ParamAccess.item);
            pManager.AddTextParameter("sBoundaryCondition", "sBoundaryCondition", "sBoundaryCondition", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Constraints", "Constraints", "Constraints", GH_ParamAccess.list);
            pManager.AddVectorParameter("ReactionForce", "ReactionForce", "ReactionForce", GH_ParamAccess.item);
            pManager.AddVectorParameter("ReactionMoment", "ReactionMoment", "ReactionMoment", GH_ParamAccess.item);
        }
        
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            sPointSupport sn = null;

            if (!DA.GetData(0, ref sn)) return;

            string modelUnit = Rhino.RhinoDoc.ActiveDoc.ModelUnitSystem.ToString();
            sRhinoConverter rhcon = new sRhinoConverter("Meters", modelUnit);
            
            
            DA.SetData(0, rhcon.EnsureUnit(rhcon.ToRhinoPoint3d(sn.location)));
            DA.SetData(1, sn.supportType.ToString());
            if(sn.constraints != null)
            {
                DA.SetDataList(2, sn.constraints.ToList());
            }
            else
            {
                DA.SetDataList(2, null);
            }
            if(sn.reaction_force != null)
            {
                DA.SetData(3, rhcon.EnsureUnit_Force(rhcon.ToRhinoVector3d(sn.reaction_force)));
            }
            else
            {
                DA.SetData(3, null);
            }
            if(sn.reaction_moment != null)
            {
                DA.SetData(4, rhcon.EnsureUnit_Moment(rhcon.ToRhinoVector3d(sn.reaction_moment)));
            }
            else
            {
                DA.SetData(4, null);
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("47d2eff2-19b1-48af-81a2-5eec40d3534c"); }
        }

        protected override System.Drawing.Bitmap Internal_Icon_24x24
        {
            get
            {
                return Resources.ToRhinoPoint;
            }
        }


    }
}

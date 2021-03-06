﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using sDataObject.sGeometry;
using sDataObject.ElementBase;
using sDataObject.IElement;

namespace sDataObject.sElement
{
    public class sFrameSet : FrameSetBase
    {
        public sFrameSet()
        {
            SetDefaults();
        }

        public sFrameSet(sCurve pCrv)
        {
            this.parentCrv = pCrv;
            SetDefaults();
        }

        void SetDefaults()
        {
            this.objectGUID = Guid.NewGuid();
            this.frames = new List<sFrame>();
            this.lineLoads = new List<sLineLoad>();

            this.parentSegments = new List<sCurve>();
        }

        public override IFrameSet DuplicatesFrameSet()
        {
            sFrameSet bs = new sFrameSet();
            bs.frameSetName = this.frameSetName;
            bs.objectGUID = this.objectGUID;
            
            bs.setId = this.setId;
            bs.parentCrv = this.parentCrv.DuplicatesCurve();
            if (this.parentSegments != null)
            {
                bs.parentSegments = new List<sCurve>();
                foreach (sCurve sg in this.parentSegments)
                {
                    bs.parentSegments.Add(sg.DuplicatesCurve());
                }
            }
            bs.crossSection = this.crossSection.DuplicatesCrosssection();

            if (this.designedCrossSections != null)
            {
                bs.designedCrossSections = new List<sCrossSection>();
                foreach (sCrossSection cs in this.designedCrossSections)
                {
                    bs.designedCrossSections.Add(cs.DuplicatesCrosssection());
                }
            }

            if (this.lineLoads != null)
            {
                bs.lineLoads = new List<sLineLoad>();
                foreach (sLineLoad ll in this.lineLoads)
                {
                    bs.lineLoads.Add(ll.DuplicatesLineLoad());
                }
            }
            if (this.parentFixityAtStart != null) bs.parentFixityAtStart = this.parentFixityAtStart.DuplicatesFixity();
            if (this.parentFixityAtEnd != null) bs.parentFixityAtEnd = this.parentFixityAtEnd.DuplicatesFixity();

            if (this.segmentFixitiesAtStart != null)
            {
                bs.segmentFixitiesAtStart = new List<sFixity>();
                foreach (sFixity f in this.segmentFixitiesAtStart)
                {
                    bs.segmentFixitiesAtStart.Add(f.DuplicatesFixity());
                }
            }

            if (this.segmentFixitiesAtEnd != null)
            {
                bs.segmentFixitiesAtEnd = new List<sFixity>();
                foreach (sFixity f in this.segmentFixitiesAtEnd)
                {
                    bs.segmentFixitiesAtEnd.Add(f.DuplicatesFixity());
                }
            }

            if (this.associatedLocations != null)
            {
                bs.associatedLocations = new List<sXYZ>();
                foreach (sXYZ lc in this.associatedLocations)
                {
                    bs.associatedLocations.Add(lc.DuplicatesXYZ());
                }
            }


            if (this.frames != null)
            {
                bs.frames = new List<sFrame>();
                foreach (sFrame sb in this.frames)
                {
                    bs.frames.Add(sb.DuplicatesFrame());
                }
            }

            if (this.results_Max != null)
            {
                bs.results_Max = this.results_Max.DuplicatesResultRange();
            }

            return bs;
        }
    }

}

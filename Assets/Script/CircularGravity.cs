/*******************************************************************************************
 *       Author: Lane Gresham, AKA LaneMax
 *         Blog: http://lanemax.blogspot.com/
 *      Version: 1.52
 * Created Date: 04/15/13 
 * Last Updated: 05/18/13
 *  
 *  Description: 
 *  
 *      The purpose of the CircularGravity script is to allow a given assigned object the 
 *      ability to have a circular gravitational force around it, whether that force is 
 *      negative or positive. CircularGravity gives the customization options you need to 
 *      set the size, power, and force. CircularGravity also has the ability to pulse a 
 *      force and lets you set the size, pulse speed, and force of the pulse. A new ability
 *      that has been added is Tag filtering, which allows you to set or filter out what tagged
 *      objects you want to effect. Also added a big feature in 1.50 that allows you to shape the
 *      form of the force to whatever form you want using triggers, giving you the ability to 
 *      effect anything within or outside the trigger shape.
 *      
 *  How To Use:
 *  
 *      Simply drag and drop / assign this script to whatever GameObject you would like the
 *      circular gravity force to emanate from.
 * 
 *  Inputs:
 * 
 *      enable: Enable/Disable CircularGravity.
 *      
 *      forcePower: Power for the force, can be negative or positive.
 *      
 *      radiusProperties->
 *          radius: Radius of the force.
 *          radiusOverride: Allows you to override the radius with 1 to 10 times the size of
 *                          the given gameobject.
 *      
 *      triggerAreaFilter->
 *          triggerArea: This is the trigger that will be used for the area filtering.
 *          triggerAreaFilterOptions: Trigger filter options.
 * 
 *      tagFilter->
 *          tagFilterOptions: Tag filter options.
 *          tagsList: Tags used for the filter option.
 *      
 *      pulseProperties->
 *          pulse: Enable a Pulse.
 *          speed: Pulsing speed if pulse if enabled.
 *          minSize: Minimum pulse size.
 *          maxSize: Maximum pulse size.
 * 
 *      DrawGravityProperties->
 *          thickness: Thinkness of the line drawn.
 *          drawGravityForce: Used to see gravity area of gravity.
 *          
 * 
*******************************************************************************************/
using UnityEngine;
using System.Collections;
using System.Linq;

public class CircularGravity : MonoBehaviour
{
    #region Properties

    //Enable/Disable CircularGravity
    public bool enable = true;

    //Power for the force, can be negative or positive
    public float forcePower = 10f;

    //Radius properties
    [System.Serializable]
    public class RadiusProperties
    {
        //Radius of the force
        public float radius = 10f;

        //Allows you to override the radius with 1 to 10 times the size of the given gameobject
        public RadiusOverride radiusOverride = RadiusOverride.Disabled;
    }
    public RadiusProperties radiusProperties;

    //Trigger Area Filter
    [System.Serializable]
    public class TriggerAreaFilter
    {
        //Trigger Object
        public Collider triggerArea;

        //Trigger Options
        public enum TriggerAreaFilterOptions
        {
            Disabled,
            OnlyEffectWithinTigger,
            DontEffectWithinTigger,
        }
        public TriggerAreaFilterOptions triggerAreaFilterOptions = TriggerAreaFilterOptions.Disabled;
    }
    public TriggerAreaFilter triggerAreaFilter;

    //Tag filtering options
    [System.Serializable]
    public class TagFilter
    {
        //Tag filter options
        public enum TagFilterOptions
        {
            Disabled,
            OnlyEffectListedTags,
            DontEffectListedTags,
        }
        public TagFilterOptions tagFilterOptions = TagFilterOptions.Disabled;

        //Tags used for the filter option
        public string[] tagsList;
    }
    public TagFilter tagFilter;

    //Pulse properties
    [System.Serializable]
    public class PulseProperties
    {
        //Enable a Pulse
        public bool pulse = false;

        //Pulsing speed if pulse if enabled
        public float speed = 0f;

        //Minimum pulse size
        public float minSize = 0f;

        //Maximum pulse size
        public float maxSize = 0f;
    }
    public PulseProperties pulseProperties;

    //Draw gravity properties
    [System.Serializable]
    public class DrawGravityProperties
    {
        //Thinkness of the line drawn
        public float thickness = 0.05f;

        //Used to see gravity area of gravity
        public bool drawGravityForce = true;
    }
    public DrawGravityProperties drawGravityProperties;

    //Used to tell whether to add or subtract to pulse
    private bool pulse_Positive;

    //Transparent Color
    private Color transparentColor;

    //Radius override options
    public enum RadiusOverride
    {
        Disabled,
        GameObjectSize_x1,
        GameObjectSize_x2,
        GameObjectSize_x4,
        GameObjectSize_x6,
        GameObjectSize_x8,
        GameObjectSize_x10,
    }

    #endregion

    #region Unity Events

    void Awake()
    {
        #region Default Settings

        //Used for when dynamically creating a CircularGracity object
        if (radiusProperties == null)
        {
            radiusProperties = new RadiusProperties();
        }
        if (triggerAreaFilter == null)
        {
            triggerAreaFilter = new TriggerAreaFilter();
        }
        if (tagFilter == null)
        {
            tagFilter = new TagFilter();
        }
        if (pulseProperties == null)
        {
            pulseProperties = new PulseProperties();
        }
        if (drawGravityProperties == null)
        {
            drawGravityProperties = new DrawGravityProperties();
        }
		
		/*
        //Sets up the line that gets rendered showing the area of forces
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetVertexCount(12);
        lineRenderer.SetWidth(0.05f, 0.05f);
		*/
        //Sets up pulse
        if (pulseProperties.pulse)
        {
            radiusProperties.radius = pulseProperties.minSize;
            pulse_Positive = true;
        }

        transparentColor = new Color();

        #endregion
    }

    //Use this for initialization
    void Start()
    {
    }

    //Update is called once per frame
    void Update()
    {
        if (enable/*&&Logic.status==Logic.GameStatus.Running*/)
        {
            CalculatePulse();
        }
    }

    //This function is called every fixed frame
    void FixedUpdate()
    {
        if (enable/*&&Logic.status==Logic.GameStatus.Running*/)
        {
            CalculateAndEstimateForce();
        }
    }

    #endregion

    #region Functions

    //Calculatie the given pulse
    private void CalculatePulse()
    {
        if (pulseProperties.pulse)
        {
            if (pulse_Positive)
            {
                if (radiusProperties.radius <= pulseProperties.maxSize)
                    radiusProperties.radius = radiusProperties.radius + (pulseProperties.speed * Time.deltaTime);
                else
                    pulse_Positive = false;
            }
            else
            {
                if (radiusProperties.radius >= pulseProperties.minSize)
                    radiusProperties.radius = radiusProperties.radius - (pulseProperties.speed * Time.deltaTime);
                else
                    pulse_Positive = true;
            }
        }
    }

    //Calculates the area to use the circular gravity
    private float CalculateRadius()
    {
        if (!pulseProperties.pulse)
        {
            float scale = 0f;

            switch (radiusProperties.radiusOverride)
            {
                case RadiusOverride.Disabled:
                    scale = radiusProperties.radius;
                    break;
                case RadiusOverride.GameObjectSize_x1:
                    scale = GetScaleSizeAvg();
                    break;
                case RadiusOverride.GameObjectSize_x2:
                    scale = GetScaleSizeAvg() * 2f;
                    break;
                case RadiusOverride.GameObjectSize_x4:
                    scale = GetScaleSizeAvg() * 4f;
                    break;
                case RadiusOverride.GameObjectSize_x6:
                    scale = GetScaleSizeAvg() * 6f;
                    break;
                case RadiusOverride.GameObjectSize_x8:
                    scale = GetScaleSizeAvg() * 8f;
                    break;
                case RadiusOverride.GameObjectSize_x10:
                    scale = GetScaleSizeAvg() * 10f;
                    break;
            }

            return scale / 2f;
        }
        else
        {
            return radiusProperties.radius / 2f;
        }
    }

    //Gets average size of current object
    private float GetScaleSizeAvg()
    {
        return (this.transform.localScale.x + this.transform.localScale.y + this.transform.localScale.z) / 3;
    }

    //Calculate and Estimate the force
    private void CalculateAndEstimateForce()
    {
        Vector3 forceLocation = this.transform.position;
        Collider[] colliders = Physics.OverlapSphere(forceLocation, CalculateRadius());

        var hits =
        from h in colliders
        select h;

        hits = hits.Where(h => h.GetComponent<Rigidbody>() != this.GetComponent<Rigidbody>());
        hits = hits.Where(h => h.GetComponent<Rigidbody>() == true);
        
        //Used for Tag filtering
        switch (tagFilter.tagFilterOptions)
        {
            case TagFilter.TagFilterOptions.Disabled:
                break;
            case TagFilter.TagFilterOptions.OnlyEffectListedTags:
                hits = hits.Where(h => tagFilter.tagsList.Contains<string>(h.tag));
                break;
            case TagFilter.TagFilterOptions.DontEffectListedTags:
                hits = hits.Where(h => !tagFilter.tagsList.Contains<string>(h.tag));
                break;
        }

        //Used for Trigger Area Filtering
        switch (triggerAreaFilter.triggerAreaFilterOptions)
        {
            case TriggerAreaFilter.TriggerAreaFilterOptions.Disabled:
                break;
            case TriggerAreaFilter.TriggerAreaFilterOptions.OnlyEffectWithinTigger:
                hits = hits.Where(h => triggerAreaFilter.triggerArea.GetComponent<Collider>().bounds.Contains(h.transform.position));
                break;
            case TriggerAreaFilter.TriggerAreaFilterOptions.DontEffectWithinTigger:
                hits = hits.Where(h => !triggerAreaFilter.triggerArea.GetComponent<Collider>().bounds.Contains(h.transform.position));
                break;
            default:
                break;
        }

        foreach (var hit in hits)
        {
            hit.GetComponent<Rigidbody>().AddExplosionForce(forcePower, forceLocation, CalculateRadius());
        }
    }

    #endregion

    #region Draw

    //Draws effected area by forces
    private void DrawGravityForce()
    {
        Color DebugGravityLineColor;

        if (forcePower == 0)
            DebugGravityLineColor = Color.white;
        else if (forcePower > 0)
            DebugGravityLineColor = Color.green;
        else
            DebugGravityLineColor = Color.red;

        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();

        lineRenderer.SetWidth(drawGravityProperties.thickness, drawGravityProperties.thickness);

        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(DebugGravityLineColor, DebugGravityLineColor);

        lineRenderer.SetPosition(0, transform.position + (Vector3.up * CalculateRadius()));
        lineRenderer.SetPosition(1, this.transform.position);

        lineRenderer.SetPosition(2, transform.position + (Vector3.down * CalculateRadius()));
        lineRenderer.SetPosition(3, this.transform.position);

        lineRenderer.SetPosition(4, transform.position + (Vector3.left * CalculateRadius()));
        lineRenderer.SetPosition(5, this.transform.position);

        lineRenderer.SetPosition(6, transform.position + (Vector3.right * CalculateRadius()));
        lineRenderer.SetPosition(7, this.transform.position);

        lineRenderer.SetPosition(8, transform.position + (Vector3.forward * CalculateRadius()));
        lineRenderer.SetPosition(9, this.transform.position);

        lineRenderer.SetPosition(10, transform.position + (Vector3.back * CalculateRadius()));
        lineRenderer.SetPosition(11, this.transform.position);
    }

    #endregion
}
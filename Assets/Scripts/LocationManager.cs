﻿

namespace Assets.Scripts
{
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    public class LocationManager : MonoBehaviour
    {

        public Dictionary<LocationType, Transform> Locations = new Dictionary<LocationType, Transform>();

        void Awake()
        {
            var saloon = GameObject.FindGameObjectWithTag("Saloon");
            if (saloon == null) throw new NullReferenceException("Saloon game object is not available!");
            
            Locations.Add(LocationType.Saloon, saloon.transform);

            var home = GameObject.FindGameObjectWithTag("Home");
            if (home == null) throw new NullReferenceException("Home game object is not available!");

            Locations.Add(LocationType.HomeSweetHome, home.transform);


            var mine = GameObject.FindGameObjectWithTag("Mine");
            if (mine == null) throw new NullReferenceException("Mine game object is not available!");

            Locations.Add(LocationType.Goldmine, mine.transform);
        }
      
    }
}

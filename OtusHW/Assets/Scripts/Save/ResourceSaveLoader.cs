using System.Collections.Generic;
using System.Linq;
using SaveSystem;
using UnityEngine;

namespace GameEngine.Save
{
    public struct ResourceData
    {
        public string Id;
        public int Amount;

        public ResourceData(Resource resource)
        {
            Amount = resource.Amount;
            Id = resource.ID;
        }
    }

    public class ResourceSaveLoader : SaveLoader<ResourceService, ResourceData[]>
    {
        protected override string DATA_KEY => "resources-data";

        public ResourceSaveLoader(ResourceService resourceService, ISerializableRepository serializableRepository):
            base(serializableRepository, resourceService)
        {
        }

        protected override ResourceData[] ConvertToData()
        {
            Resource[] resources = _dataService.GetResources().ToArray();
            ResourceData[] resourcesData = new ResourceData[resources.Length];

            for (int i = 0; i < resources.Length; i++)
            {
                resourcesData[i] = new ResourceData(resources[i]);
            }

            return resourcesData;
        }

        protected override void SetupData(ResourceData[] resourcesSet)
        {
            HashSet<Resource> existingResources = _dataService.GetResources().ToHashSet();

            for (var i = 0; i < resourcesSet.Length; i++)
            {
                string requiredId = resourcesSet[i].Id;

                Resource selectedResource = existingResources.FirstOrDefault(u => u.ID == requiredId);

                if (selectedResource == null)
                {
                    //throw new NullReferenceException("No resource with ID: " + requiredId);
                    Debug.LogWarning("No available resource to load with ID: " + requiredId);
                }
                else
                {
                    existingResources.Remove(selectedResource);
                }

                selectedResource.Setup(resourcesSet[i]);
            }
        }
    }
}
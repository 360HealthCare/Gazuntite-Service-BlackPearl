namespace EprescriptionService.Utility
{
    /// <summary>
    ///   Class:          SystemConstants
    ///   Created by:     Waqas Baber 
    ///   Description:    MessageTypes for detecting appropriate message formats to load 
    ///   Notes:          
    ///   Copyright 2011 Zigron Inc. All rights reserved  
    /// </summary>
    public enum  MessageTypes
    {

        /// <summary>
        /// To Download prescription for a patient
        /// </summary>
        UpdatePrescription,
        /// <summary>
        /// To Download medication for a patient 
        /// </summary>
        UpdateMedication,
        /// <summary>
        /// To Download alergies for a patient
        /// </summary>
        UpdateAlergies,
        /// <summary>
        /// To Download problems for a patient
        /// </summary>
        UpdatProblems,
        /// <summary>
        /// Upload patient 
        /// </summary>
        SendPatient
    }
}
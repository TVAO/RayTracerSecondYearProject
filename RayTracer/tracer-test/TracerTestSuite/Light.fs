﻿namespace TracerTestSuite

open API.API
open System
open System.Drawing

module Light =

  let private folder = "light"

  let renderSphere lightPos name toScreen =
    let light = mkLight lightPos (fromColor Color.White) 0.9 in
    let ambientLight = mkAmbientLight (fromColor Color.White) 0.1 in
    let camera = mkCamera (mkPoint 0.0 0.0 12.0) (mkPoint 0.0 0.0 0.0) (mkVector 0.0 1.0 0.0) 10.0 2.0 2.0 500 500 in
    let sphere = mkSphere (mkPoint 0.0 0.0 0.0) 1.0 (mkMatTexture (mkMaterial (fromColor Color.Blue) 0.0)) in
    let scene = mkScene [sphere] [light] ambientLight camera 0 in
    Util.render' scene (folder, "sphere " + name + ".png") toScreen

  let renderSphereFront = renderSphere (mkPoint 0.0 0.0 4.0) "front"
  let renderSphereTop = renderSphere (mkPoint 0.0 4.0 0.0) "top"
  let renderSphereFrontFar = renderSphere (mkPoint 0.0 0.0 40.0) "front far"
  let renderSphereTopFar = renderSphere (mkPoint 0.0 40.0 0.0) "top far"


  let renderSphereColour toScreen =
    let lightLeft = mkLight (mkPoint -4.0 -2.0 2.0) (fromColor Color.Red) 0.8 in
    let lightRight = mkLight (mkPoint 4.0 -2.0 2.0) (fromColor Color.Green) 0.8 in
    let lightTop = mkLight (mkPoint 0.0 4.0 2.0) (fromColor Color.Blue) 0.8 in
    let lightBottom = mkLight (mkPoint 0.0 -4.0 2.0) (fromColor Color.White) 0.4 in
    
    let ambientLight = mkAmbientLight (fromColor Color.White) 0.0 in
    let camera = mkCamera (mkPoint 0.0 0.0 12.0) (mkPoint 0.0 0.0 0.0) (mkVector 0.0 1.0 0.0) 10.0 2.0 2.0 500 500 in
    let sphere = mkSphere (mkPoint 0.0 0.0 0.0) 1.0 (mkMatTexture (mkMaterial (fromColor Color.Orange) 0.0)) in
    let scene = mkScene [sphere] [lightLeft;lightRight;lightTop;lightBottom] ambientLight camera 0 in
    Util.render' scene (folder, "sphere colour.png") toScreen

  
  let renderShadow toScreen =
    let light = mkLight (mkPoint 1.5 -1.5 4.0) (fromColor Color.Cyan) 0.8 in
    let light2 = mkLight (mkPoint 0.0 0.0 4.0) (fromColor Color.Red) 0.6 in
    let ambientLight = mkAmbientLight (fromColor Color.White) 0.1 in
    let camera = mkCamera (mkPoint 0.0 0.0 28.0) (mkPoint 0.0 0.0 0.0) (mkVector 0.0 1.0 0.0) 10.0 2.0 2.0 500 500 in
    let sphere = mkSphere (mkPoint 0.0 0.0 0.0) 1.0 (mkMatTexture (mkMaterial (fromColor Color.Blue) 0.0)) in
    let redMat = mkMaterial (fromColor Color.Red) 0.0
    let red = mkMatTexture redMat
    let cylinder = mkSolidCylinder (mkPoint 1.0 -1.0 2.0) 0.3 0.8 red red red in
    let greenMat = mkMaterial (fromColor Color.Green) 0.0
    let checker x y =
        let abs' f = if f < 0.0 then 1.0 - (f*2.0) else f * 2.0
        if (int (abs' x) + int (abs' y)) % 2 = 0
        then redMat
        else greenMat
    let plane =  transform (mkPlane (mkTexture checker)) 
                  (mergeTransformations [rotateY (Math.PI/4.0);rotateX (Math.PI/4.0);(translate -4.0 0.0 -4.0)])
    let scene = mkScene [sphere;cylinder;plane] [light;light2] ambientLight camera 0 in
    Util.render' scene (folder, "shadow.png") toScreen
  

  let render toScreen =
    renderSphereFront toScreen;
    renderSphereTop toScreen;
    renderSphereFrontFar toScreen;
    renderSphereTopFar toScreen;
    renderSphereColour toScreen;
    renderShadow toScreen;

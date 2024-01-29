namespace HezekDev.ServiceBuilder {
    public enum EServiceNodeType : byte {
        NODE_SERVICE_HOLDER = 0x001, // in this case, this is the holder of the node 
        NODE_SERVICE_CHILDREN = 0x002, // in this case, this is the service of the node
    }
}
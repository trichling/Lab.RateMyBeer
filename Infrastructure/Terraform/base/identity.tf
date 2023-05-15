#resource "azurerm_user_assigned_identity" "identity" {
#  resource_group_name = azurerm_resource_group.RateMyBeerRessourceGroup.name
#  location            = azurerm_resource_group.RateMyBeerRessourceGroup.location
#  name                = "${var.environment}-${var.application}"
#}


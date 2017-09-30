﻿using Content.Server.Interfaces.GameObjects;
using SS14.Shared.GameObjects;
using SS14.Shared.Interfaces.GameObjects;
using SS14.Server.Interfaces.GameObjects;
using System;

namespace Content.Server.GameObjects
{
    public class ItemComponent : Component, IItemComponent
    {
        public override string Name => "Item";

        /// <inheritdoc />
        public IInventorySlot ContainingSlot { get; private set; }

        public void RemovedFromSlot()
        {
            if (ContainingSlot == null)
            {
                throw new InvalidOperationException("Item is not in a slot.");
            }

            ContainingSlot = null;

            foreach (var component in Owner.GetComponents<ISpriteRenderableComponent>())
            {
                component.Visible = true;
            }
        }

        public void EquippedToSlot(IInventorySlot slot)
        {
            if (ContainingSlot != null)
            {
                throw new InvalidOperationException("Item is already in a slot.");
            }

            ContainingSlot = slot;

            foreach (var component in Owner.GetComponents<ISpriteRenderableComponent>())
            {
                component.Visible = false;
            }
        }
    }
}